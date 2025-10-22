using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;
using UnityEngine.Events;
namespace Enemy
{
    public class EnemyBase : MonoBehaviour,IDamageable
    {
        [SerializeField]
        private AnimationBase _animationBase;

        public FlashColor flashColor;

        public float startLife = 10f;

        [SerializeField]
        protected float _currentLife;

        [Header("Start Animation")]

        public float startAnimationDuration = 0.2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        public Collider coll;

        public ParticleSystem particleSystem;
        
        [Header("Animation Exclusive")]
        [SerializeField]
        private bool isChicken;

        [SerializeField]
        protected float reSize;

        [SerializeField]
        protected float durationSize;

        [SerializeField]
        protected float minScaleSize;

        public Transform graphic;
        public ParticleSystem deathParticle;

        public bool lookAtPlayer;

        private Player _player;

        [Header("Events")]
        public UnityEvent OnKillEvent;
        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }
        protected virtual void Init()
        {
            ResetLife();
            
            if (startWithBornAnimation)
            {
                BornAnimation();
            }
            
        }
        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            //Destroy(gameObject,3);
            
            if (coll != null)
            {
                coll.enabled = false;
            }
            PlayAnimationByTrigger(AnimationType.DEATH);
            deathParticle.Play();
            Destroy(gameObject, 3);
            OnKillEvent?.Invoke();
        }

        public virtual void OnDamage(float dmg,bool antiChicken)
        {
            if(flashColor != null)
            {
                flashColor.Flash();
            }

            if(particleSystem != null)
            {
                particleSystem.Emit(15);
            }

            transform.position -= transform.forward;

            _currentLife -= dmg;

            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        public void Damage(float damage,bool antiChicken)
        {
            OnDamage(damage,antiChicken);
        }

        public void Damage(float damage)
        {
            //Do nothing here
        }

        public void Damage(float damage,bool antiChicken,Vector3 dir)
        {
            OnDamage(damage, antiChicken);
            transform.DOMove(transform.position - dir, 0.1f);
            //transform.position -= dir;
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
            Player p = collision.gameObject.GetComponent<Player>();

            if(p != null)
            {
                p.healthBase.Damage(1);
            }
        }

        public virtual void Update()
        {
            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }
        
    }
}
