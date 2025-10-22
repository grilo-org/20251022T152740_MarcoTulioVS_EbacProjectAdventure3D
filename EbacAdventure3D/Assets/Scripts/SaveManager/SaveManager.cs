using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Ebac.Core.Singleton;
using Itens;
using System;
public class SaveManager : Singleton<SaveManager>
{

    [SerializeField] private SaveSetup _saveSetup;
    //private string path = Application.dataPath + "/save.txt";
    private string path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;
    public Vector3 lastPosition;
    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup { get { return _saveSetup; } }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Mark";
    }
    private void Start()
    {
        Invoke(nameof(Load), .1f);
        
    }

    [NaughtyAttributes.Button]
    public void Save()
    {
       
        string setupToJson = JsonUtility.ToJson(_saveSetup,true);
        //Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.coins = ItemManager.instance.GetItemByType(ItemType.COIN).soInt.value;
        _saveSetup.health = ItemManager.instance.GetItemByType(ItemType.LIFE_PACK).soInt.value;
        Save();
    }

    private void SaveFile(string json)
    {
        

        //string fileLoaded = "";

        //if (File.Exists(path))
        //{
        //    fileLoaded = File.ReadAllText(path);
        //}

        File.WriteAllText(path,json);
    }

    [NaughtyAttributes.Button]
    private void Load()
    {
        
        string fileLoaded = "";
        
        if (File.Exists(path))
        {
            fileLoaded = File.ReadAllText(path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);

            lastPosition = _saveSetup.lastPosition;
            lastLevel = _saveSetup.lastLevel;
            
            
            //lastPosition.x = _saveSetup.lastPositionX;
            //lastPosition.y = _saveSetup.lastPositionY;
            //lastPosition.z = _saveSetup.lastPositionZ;

        }
        else
        {
            CreateNewSave();
            Save();
        }

        FileLoaded?.Invoke(_saveSetup);

    }
    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();
        
    }

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }

    [NaughtyAttributes.Button]
    private void SaveLevelFive()
    {
        SaveLastLevel(5);
    }

    private void OnApplicationQuit()
    {
        Save();
        
    }

    
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;

    public float coins;
    public float health;

    //public float lastPositionX;
    //public float lastPositionY;
    //public float lastPositionZ;
    public Vector3 lastPosition;
    public Texture texture;
}
