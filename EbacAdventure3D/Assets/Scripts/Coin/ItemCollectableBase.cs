using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itens
{
    public class ItemCollectableBase : MonoBehaviour
    {
        public SFXType sfxType;
        public ItemType itemType;

        public string compareTag = "Player";
        public ParticleSystem particleSystem;

        public float timeToHide = 1f;

        public GameObject graphicItem;

        [Header("Sounds")]
        public AudioSource audioSource;


        public Collider collider;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == compareTag)
            {
                Collect();
            }
        }
        protected virtual void Collect()
        {
            PlaySFX();

            if(collider != null)
            {
                collider.enabled = false;
            }

            if (graphicItem != null)
            {
                graphicItem.SetActive(false);
            }

            Invoke("HideObject", timeToHide);
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);

        }
        protected virtual void OnCollect()
        {
            if (particleSystem != null)
            {
                particleSystem.Play();

                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }

            ItemManager.instance.AddByType(itemType);
        }

        private void PlaySFX()
        {
            SFXPool.instance.Play(sfxType);
        }
    }
}
