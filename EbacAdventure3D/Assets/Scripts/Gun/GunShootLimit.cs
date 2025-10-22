using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GunShootLimit : GunBase
{
    public float maxShoot = 5f;
    public float timeToRecharge = 1f;

    private float _currentShoots;

    private bool _recharging;

    public List<UIFillUpdater> uiGunUpdaters;

    private void Awake()
    {
        GetAllUI();
    }
    protected override IEnumerator ShootCoroutine()
    {
        if (_recharging)
        {
            yield break;
        }

        while (true)
        {
            if (_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeToRecharge);
            }
        }
    }

    private void CheckRecharge()
    {
        if(_currentShoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;

        while (time < timeToRecharge)
        {
            time+=Time.deltaTime;
            uiGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _recharging=false;
    }

    private void UpdateUI()
    {
        uiGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }

    private void GetAllUI()
    {
        uiGunUpdaters = GameObject.FindObjectsOfType<UIFillUpdater>().ToList();
    }
}
