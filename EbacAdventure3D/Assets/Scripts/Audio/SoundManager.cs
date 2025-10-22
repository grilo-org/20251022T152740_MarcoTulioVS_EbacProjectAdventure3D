using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;

    public AudioSource musicSource;

    public AudioListener audioListener;

    public List<AudioSource> audioSources;
    [SerializeField]
    private int index;
    public void PlayMusicByType(MusicType musicType)
    {
        var music  = GetMusicByType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.type == musicType);
    }
    public SFXSetup GetSFXByType(SFXType sfxType)
    {
        return sfxSetups.Find(i => i.type == sfxType);
    }

    public void MuteAudio()
    {
        audioListener.enabled = !audioListener.enabled;

        foreach(var audio in audioSources)
        {
            audio.enabled = !audio.enabled;
        }
        
    }

    public void NextMusic()
    {
        index++;

        if (index > musicSetups.Count - 1)
        {
            index=0;
        }
        
        musicSource.clip = musicSetups[index].audioClip;
        musicSource.Play();
        
        
    }
}

[System.Serializable]
public class MusicSetup
{
    public MusicType type;
    public AudioClip audioClip;
}

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03,
    TYPE_04,
}

[System.Serializable]
public class SFXSetup
{
    public SFXType type;
    public AudioClip audioClip;
}

public enum SFXType
{
    NONE,
    TYPE_01,
    TYPE_02,
    TYPE_03,
    TYPE_04
}

