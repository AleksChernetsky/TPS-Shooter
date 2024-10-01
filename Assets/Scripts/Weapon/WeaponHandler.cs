using System.Collections.Generic;

using UnityEngine;

public class WeaponHandler
{
    private Transform _weaponPlace;
    private IKController _ikController;
    private List<Weapon> _weapons = new List<Weapon>();

    public Weapon CurrentWeapon { get; private set; }
    public bool IsArmed { get; private set; }

    public WeaponHandler(Transform weaponPlace, IKController ikController)
    {
        _weaponPlace = weaponPlace;
        _ikController = ikController;
    }

    public void EquipNewWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
        weapon.transform.SetParent(_weaponPlace);
        weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        weapon.gameObject.SetActive(false);
        weapon.gameObject.layer = LayerMask.NameToLayer("Weapon");
    }
    public Weapon TakeWeapon(int weaponIndex)
    {
        if (_weapons.Count <= (weaponIndex))
            return null;

        IsArmed = true;
        _weapons[weaponIndex].gameObject.SetActive(true);
        _ikController.SetIKWeight(IsArmed ? 1 : 0, _weapons[weaponIndex].RightHandTarget, _weapons[weaponIndex].LeftHandTarget);
        return CurrentWeapon = _weapons[weaponIndex];
    }
    public void HideWeapon(Weapon weapon)
    {
        IsArmed = false;
        weapon.gameObject.SetActive(false);
        _ikController.SetIKWeight(IsArmed ? 1 : 0, weapon.RightHandTarget, weapon.LeftHandTarget);
    }
    public void DropWeapon(Weapon weapon)
    {
        weapon.transform.SetParent(null);
    }
}
