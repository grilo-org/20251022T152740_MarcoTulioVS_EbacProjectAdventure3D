using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;
public class ActionLifePack : MonoBehaviour
{
    public SOInt soInt;

    public KeyCode keycode = KeyCode.L;
    private void Start()
    {
        soInt = ItemManager.instance.GetItemByType(ItemType.LIFE_PACK).soInt;
        
    }

    public void RecoverLife()
    {
        if (soInt.value > 0)
        {
            ItemManager.instance.RemoveByType(ItemType.LIFE_PACK);
            Player.instance.healthBase.ResetLife();

        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            RecoverLife();
        }
    }
}
