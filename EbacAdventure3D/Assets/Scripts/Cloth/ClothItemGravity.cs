using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemGravity : ClothItemBase
    {
        
        public override void Collect()
        {
            base.Collect();
            Player.instance.ChangeGravity(true,duration);
        }
    }
}
