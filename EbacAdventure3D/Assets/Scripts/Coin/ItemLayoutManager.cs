using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itens
{
    public class ItemLayoutManager : MonoBehaviour
    {
        public ItemLayout prefabLayout;
        public Transform container;
        
        public List<ItemLayout> itemLayouts;

        private void Start()
        {
            CreateItems();
        }
        public void CreateItems()
        {
            foreach(var setup in ItemManager.instance.itemSetups)
            {
                var item = Instantiate(prefabLayout,container);
                item.Load(setup);
                itemLayouts.Add(item);
            }
        }

    }
}
