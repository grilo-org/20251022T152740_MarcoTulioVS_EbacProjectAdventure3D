using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;

    public float shakeDuration = 0.1f;
    public int shakeForce = 1;

    public int dropCoinAmount;
    public GameObject coinPrefab;

    public Transform dropPosition;

    private void OnValidate()
    {
        if (healthBase == null)
        {
            healthBase = GetComponent<HealthBase>();
        }
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up, shakeForce);
        //DropCoins();
        DropGroupOfCoins();
    }

    [NaughtyAttributes.Button]
    private void DropCoins()
    {
        var i = Instantiate(coinPrefab);
        i.transform.position = dropPosition.position;
        i.transform.DOScale(0,1f).SetEase(Ease.OutBack).From();
    }

    [NaughtyAttributes.Button]
    private void DropGroupOfCoins()
    {
        StartCoroutine("DropGroupOfCoinsCoroutine");
    }

    IEnumerator DropGroupOfCoinsCoroutine()
    {
        for (int i = 0; i < dropCoinAmount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(0.1f);
        }

    }
}
