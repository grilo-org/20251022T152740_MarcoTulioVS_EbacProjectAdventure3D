using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    private string checkpointKey = "CheckpointKey";

    private bool checkpointActived;

    public SFXType sfxType;
    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (!checkpointActived && other.gameObject.tag == "Player")
        {
            CheckCheckpoint();
        }
        
    }

    private void CheckCheckpoint()
    {
        TurnItOn();
        SaveCheckpoint();
        SavePosition();
    }

    [NaughtyAttributes.Button]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_Color", Color.yellow);
        PlaySound();
        
    }

    [NaughtyAttributes.Button]
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_Color", Color.white);
    }

    private void SaveCheckpoint()
    {
        //if (PlayerPrefs.GetInt(checkpointKey) > key)
        //{
        //    PlayerPrefs.SetInt(checkpointKey, key);
        //}

        CheckpointManager.instance.SaveCheckpoint(key);
        checkpointActived = true;
    }

    private void SavePosition()
    {
        SaveManager.instance.Setup.lastPosition = transform.position;
        
        SaveManager.instance.Save();
    }

    public void PlaySound()
    {
        var sfxChest = SoundManager.instance.GetSFXByType(sfxType);
        audioSource.clip = sfxChest.audioClip;
        audioSource.Play();
    }
}
