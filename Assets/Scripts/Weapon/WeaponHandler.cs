using System;
using System.Collections.Generic;

using UnityEngine;

public class WeaponHandler
{
    private List<Weapon> _weapons = new List<Weapon>();
    private Transform _weaponHolder;
    private RigController _rigController;

    public Weapon CurrentWeapon { get; private set; }
    public bool IsArmed { get; private set; }
    public bool IsAiming { get; private set; }
    public bool IsShooting { get; private set; }

    public event Action OnShootAction;

    public WeaponHandler(Transform weaponHolder, RigController rigController)
    {
        _weaponHolder = weaponHolder;
        _rigController = rigController;
    }

    public void Initialize()
    {
        PrepareWeapon();
        _rigController.SetHandsWeight(CurrentWeapon);
    }

    public void Shoot(bool shootInput)
    {
        if (!IsArmed)
            return;

        IsShooting = shootInput;
        CurrentWeapon.PerformAttack(IsShooting);
    }
    public void OnShoot() => OnShootAction?.Invoke();
    public void Aim(bool runInput, bool aimInput)
    {
        if (!IsArmed)
            return;

        IsAiming = aimInput;
        CurrentWeapon.IsAiming = !runInput && aimInput;
    }
    public void EquipNewWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
        weapon.transform.SetParent(_weaponHolder);
        weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        weapon.gameObject.SetActive(false);
        weapon.gameObject.layer = LayerMask.NameToLayer("Weapon");
    }
    public Weapon TakeWeapon(int weaponIndex)
    {
        if (_weapons.Count <= weaponIndex)
            return null;
        if (CurrentWeapon != null)
            HideWeapon();

        IsArmed = true;
        CurrentWeapon = _weapons[weaponIndex];
        CurrentWeapon.gameObject.SetActive(true);
        CurrentWeapon.OnShotEvent += OnShoot;
        _rigController.SetHandsWeight(CurrentWeapon);
        return CurrentWeapon;
    }
    public void HideWeapon()
    {
        IsArmed = false;
        IsAiming = false;
        IsShooting = false;

        CurrentWeapon.gameObject.SetActive(false);
        CurrentWeapon.OnShotEvent -= OnShoot;
        CurrentWeapon = null;
        _rigController.SetHandsWeight(CurrentWeapon);
    }
    public void DropWeapon()
    {
        HideWeapon();
        CurrentWeapon.transform.SetParent(null);
    }
    private void PrepareWeapon()
    {
        foreach (var weapon in _weaponHolder.GetComponentsInChildren<Weapon>())
        {
            if (weapon != null)
            {
                EquipNewWeapon(weapon);
                TakeWeapon(0);
            }
        }
    }
}
