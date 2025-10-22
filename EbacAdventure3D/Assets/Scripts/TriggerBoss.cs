using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss;
public class TriggerBoss : MonoBehaviour
{
    public BossBase boss;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            boss.SwitchState(BossAction.ATTACK);
        }
    }

   
}
