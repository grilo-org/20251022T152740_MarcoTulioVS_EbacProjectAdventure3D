using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
public class UIInGameManager : Singleton<UIInGameManager>
{
    public TextMeshProUGUI uiTextCoins;
    public TextMeshProUGUI uiTextPlanets;
    //public void UpdateTextCoins(TextMeshProUGUI text,int soInt)
    //{
      
    //    text.text = soInt.ToString();
        
    //}

    public void UpdateCoins(string s)
    {
        uiTextCoins.text = s;
    }

}
