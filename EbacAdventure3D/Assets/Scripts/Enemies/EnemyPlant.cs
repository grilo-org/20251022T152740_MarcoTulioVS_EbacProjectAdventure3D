using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using DG.Tweening;
public class EnemyPlant : EnemyBase
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

        _currentLife -= dmg;

        if (graphic.localScale.x > minScaleSize)
        {
            graphic.DOScale(reSize, durationSize).SetEase(startAnimationEase);
            reSize -= 1;
        }

        if (_currentLife <= 0)
        {
            Kill();
        }

    }
}
