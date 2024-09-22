using System.Collections.Generic;

using UnityEngine;

public class EquipWeaponHandler : MonoBehaviour
{
    [SerializeField] private Transform _weaponPlace;
    private RigPositionHandler _rigHandler;
    private List<Weapon> _weapons = new List<Weapon>();

    private void Start()
    {
        _rigHandler = GetComponent<RigPositionHandler>();
    }
    public void EquipNewWeapon(RaycastHit raycastHit)
    {
        if (raycastHit.collider.TryGetComponent(out Weapon weapon) && Vector3.Distance(transform.position, raycastHit.point) < 2f)
        {
            _weapons.Add(weapon);
            weapon.transform.SetParent(_weaponPlace);
            weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            weapon.gameObject.SetActive(false);
            weapon.gameObject.layer = LayerMask.NameToLayer("Weapon");
        }
    }
    public Weapon TakeWeapon(int weaponIndex)
    {
        if (_weapons.Count <= (weaponIndex - 1))
            return null;

        _weapons[weaponIndex - 1].gameObject.SetActive(true);
        _rigHandler.SetLeftHand(_weapons[weaponIndex - 1]);
        return _weapons[weaponIndex - 1];
    }
    public void HideWeapon(Weapon weapon)
    {
        weapon.gameObject.SetActive(false);
        weapon = null;
        _rigHandler.SetLeftHand(weapon);
    }
    public void DropWeapon(Weapon weapon)
    {
        weapon.transform.SetParent(null);
    }
}
