using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using DG.Tweening;
public class EnemyChicken : EnemyBase
{
    public override void OnDamage(float dmg, bool antiChicken)
    {
        if (flashColor != null)
        {
            flashColor.Flash();
        }

        if (particleSystem != null)
        {
            particleSystem.Emit(15);
        }

        if (antiChicken)
        {
            _currentLife -= dmg;

        }
        else
        {

            if (transform.localScale.x < 5)
            {
                _currentLife += dmg;
                transform.DOScale(reSize, durationSize).SetEase(startAnimationEase);
                reSize += 1;

            }

        }

        if (_currentLife <= 0)
        {
            Kill();
        }

    }
}
