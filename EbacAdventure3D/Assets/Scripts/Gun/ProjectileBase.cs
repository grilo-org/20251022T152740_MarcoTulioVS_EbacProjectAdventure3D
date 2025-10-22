using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    
    public float timeToDestroy = 2f;

    public float speed = 50f;

    public float damageAmount;

    [SerializeField]
    private bool antiChicken;

    public List<string> tagsToHit;
    private void Awake()
    {
        Destroy(gameObject,timeToDestroy);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var t in tagsToHit)
        {

            if (collision.transform.tag == t)
            {

                var damageable = collision.gameObject.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    ShakeCamera.instance.Shake();
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0f;
                    damageable.Damage(damageAmount, antiChicken, dir);
                }
                break;
            }
        }
        
        Destroy(gameObject);
        
    }

}
