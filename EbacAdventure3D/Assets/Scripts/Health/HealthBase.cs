using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cloth;
public class HealthBase : MonoBehaviour,IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill;

    [SerializeField]
    private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<UIFillUpdater> uIGunUpdater;

    public float damageMultiply = 1;

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        ResetLife();
    }
    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        if (destroyOnKill)
        {
            Destroy(gameObject, 3f);
        }
        OnKill?.Invoke(this);
    }

    public virtual void Damage(float dmg)
    {
       

        //transform.position -= transform.forward;

        _currentLife -= dmg * damageMultiply;

        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    //Debug
    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }

    public void Damage(float damage, bool antiChicken)
    {
        
    }

    public void Damage(float damage, bool antiChicken, Vector3 dir)
    {
        Damage(damage);
    }

    public void UpdateUI()
    {
        if(uIGunUpdater != null)
        {
            uIGunUpdater.ForEach(i=>i.UpdateValue((float)_currentLife / startLife));
        }
    }

    public void ChangeDamageMultiply(float damage, float duration)
    {
        StartCoroutine(ChangeDamageCoroutine(damageMultiply, duration));
    }

    IEnumerator ChangeDamageCoroutine(float damageMultiply, float duration)
    {
        this.damageMultiply = damageMultiply;
        yield return new WaitForSeconds(duration);
        this.damageMultiply = 1;
    }
}

