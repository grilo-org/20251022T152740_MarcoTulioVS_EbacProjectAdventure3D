using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Animation;
public class EnemyBomb : EnemyBase
{
    private bool playerTouch;
    public override void OnCollisionEnter(Collision collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();

        if (p != null)
        {

            StartCoroutine(PlayExplosionAnimation());
            p.healthBase.Damage(10);
            Destroy(gameObject, 2);
        }
    }

    IEnumerator PlayExplosionAnimation()
    {
        if (!playerTouch)
        {
            playerTouch = true;
            PlayAnimationByTrigger(AnimationType.ATTACK);
            yield return new WaitForSeconds(2);
            playerTouch = false;
        }
        
    }
}
