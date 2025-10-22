using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;
public class PlayerMagneticTrigger : MonoBehaviour
{
    public Collider player;
    private void OnTriggerEnter(Collider other)
    {
        ItemCollectableBase i = other.GetComponent<ItemCollectableBase>();

        if(i != null)
        {
            i.gameObject.AddComponent<Magnetic>();
            Physics.IgnoreCollision(player, other);
            
        }
    }
}
