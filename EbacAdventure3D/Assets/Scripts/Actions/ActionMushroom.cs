using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;
public class ActionMushroom : MonoBehaviour
{
    public SOInt soInt;

    public KeyCode KeyCode = KeyCode.M;

    public int increaseSizeAmount;
    private void Start()
    {
        soInt = ItemManager.instance.GetItemByType(ItemType.MUSHROOM).soInt;
    }

    public void BecomeGiant()
    {
        if (soInt.value > 0)
        {
            ItemManager.instance.RemoveByType(ItemType.MUSHROOM);
            Player.instance.IncraseSize(increaseSizeAmount);
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode))
        {
            BecomeGiant();
        }
    }
}
