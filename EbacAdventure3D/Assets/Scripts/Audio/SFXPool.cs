using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
public class SFXPool : Singleton<SFXPool>
{
    public List<AudioSource> audioSourceList;

    public int poolSize = 10;

    private int _index;

    private void Start()
    {
        CreatePool();
    }
    private void CreatePool()
    {
        audioSourceList = new List<AudioSource>();
        
        for(int i = 0; i<poolSize; i++)
        {
            CreateAudioSourceItem();
        }
    }
    
    private void CreateAudioSourceItem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        audioSourceList.Add(go.AddComponent<AudioSource>());
    }

    public void Play(SFXType sfxType)
    {
        if (sfxType == SFXType.NONE)
        {
            return;
        }

        var sfx = SoundManager.instance.GetSFXByType(sfxType);

        audioSourceList[_index].clip = sfx.audioClip;
        audioSourceList[_index].Play();

        _index++;

        if (_index >= audioSourceList.Count)
        {
            _index = 0;
        }
    }
}
