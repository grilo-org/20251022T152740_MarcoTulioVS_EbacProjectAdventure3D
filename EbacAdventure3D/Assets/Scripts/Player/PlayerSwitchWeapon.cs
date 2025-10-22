using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchWeapon : PlayerAbilityBase
{
    [SerializeField]
    private PlayerAbilityShoot playerAbilityShoot;

    public List<GunBase> allGuns;
    protected override void Init()
    {
        base.Init();
        inputs.Gameplay.Weapon.performed += ctx => SwitchWeapon(allGuns[0]);
        inputs.Gameplay.Weapon2.performed += ctx => SwitchWeapon(allGuns[1]);
    }

    private void SwitchWeapon(GunBase gun)
    {
        playerAbilityShoot.gunBase = gun;
        playerAbilityShoot.CreateGun();
    }
}
