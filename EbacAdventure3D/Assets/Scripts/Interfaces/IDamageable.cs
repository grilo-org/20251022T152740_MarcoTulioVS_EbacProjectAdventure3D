using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(float damage);
    void Damage(float damage,bool antiChicken);
    void Damage(float damage, bool antiChicken,Vector3 dir);
}
