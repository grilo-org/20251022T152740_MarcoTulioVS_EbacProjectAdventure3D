using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartCheck : MonoBehaviour
{

    public string tagToCheck = "Player";

    public GameObject bossCamera;


    private void Awake()
    {
        bossCamera.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tagToCheck)
        {
            TurnCameraOn();
        }
    }

    public void TurnCameraOn()
    {
        bossCamera.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }
}
