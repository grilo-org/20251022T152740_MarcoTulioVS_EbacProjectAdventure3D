using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunThrowEnemy : GunBase
{
    public override void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;
    }

}
