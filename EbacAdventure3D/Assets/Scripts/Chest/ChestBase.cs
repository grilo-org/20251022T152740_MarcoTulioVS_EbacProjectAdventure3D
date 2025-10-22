using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Itens;
public class ChestBase : MonoBehaviour
{
    public KeyCode keycode = KeyCode.Z;

    public Animator animator;
    public string triggerOpen = "Open";

    [Header("Notification")]

    public GameObject notification;
    public float tweenDuration = 0.2f;
    public Ease ease = Ease.OutBack;
    private float startScale;

    private bool _openedChest;

    public ChestItemBase chestItem;

    public SFXType sfxType;
    public AudioSource audioSource;
    
    private void Start()
    {
        startScale = notification.transform.localScale.x;
        HideNotification();
    }

    [NaughtyAttributes.Button]
    public void OpenChest()
    {
        if (_openedChest) return;
        
        var sfxChest = SoundManager.instance.GetSFXByType(sfxType);
        audioSource.clip = sfxChest.audioClip;
        audioSource.Play();

        animator.SetTrigger(triggerOpen);
        _openedChest = true;
        HideNotification();
        Invoke(nameof(ShowItem), 1f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), 1f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();

        if (p != null)
        {
            ShowNotification();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player p = other.GetComponent<Player>();

        if (p != null)
        {
            HideNotification();
        }
    }

    [NaughtyAttributes.Button]
    public void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale,tweenDuration);
    }

    [NaughtyAttributes.Button]
    public void HideNotification()
    {
        notification.SetActive(false);
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    private void Update()
    {
        if(Input.GetKeyDown(keycode) && notification.activeSelf)
        {
            OpenChest();
        }
    }
}
