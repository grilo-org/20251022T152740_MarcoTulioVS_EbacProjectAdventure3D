using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itens
{
    public class ItemCollectablePlanet : ItemCollectableBase
    {
        protected override void OnCollect()
        {
            base.OnCollect();
            //ItemManager.instance.AddPlanet();
        }
    }
}
