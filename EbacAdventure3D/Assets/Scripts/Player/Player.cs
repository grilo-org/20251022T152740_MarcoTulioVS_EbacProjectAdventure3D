using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Cloth;
public class Player : Singleton<Player>/*,IDamageable*/
{
    public CharacterController characterController;
    public List<Collider> colliders;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;

    private float vSpeed = 0;

    public float jumpSpeed;
    public Animator animator;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    public HealthBase healthBase;

    private bool _alive = true;

    public UIFillUpdater uiGunUpdater;

    [Header("Skin")]
    [SerializeField]
    private ClothChange _clothChange;

    private bool invertGravity = false;

    private bool _isJumping;

    private void OnValidate()
    {
        if(healthBase == null)
        {
            healthBase = GetComponent<HealthBase>();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();
        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;

        
    }

    //private void Awake()
    //{
    //    OnValidate();

    //    healthBase.OnDamage += Damage;
    //    healthBase.OnKill += OnKill;
    //}

    private void Start()
    {
        if (SaveManager.instance.Setup.lastPosition == Vector3.zero)
        {
            
            SaveManager.instance.Setup.lastPosition = new Vector3(411.89f, -7.59f, 16.9f);
        }
        
        transform.position = SaveManager.instance.Setup.lastPosition;
        

    }

    
    private void Update()
    {
        transform.Rotate(0,Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime,0);
        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded)
        {
            if (_isJumping)
            {
                _isJumping = false;
                animator.SetTrigger("Land");
            }

            vSpeed = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                vSpeed = jumpSpeed;

                if (!_isJumping)
                {
                    _isJumping = true;
                    animator.SetTrigger("Jump");
                }
                
            }
        }


        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        var isWalking = inputAxisVertical != 0;

        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);
        animator.SetBool("Run", inputAxisVertical != 0);

        ApplyGravityChange();


    }

    public void Damage(float damage, bool antiChicken)
    {
       //Do nothing here
    }

    public void Damage(HealthBase healthBase, bool antiChicken, Vector3 dir)
    {
        Damage(healthBase);
        
    }

    public void Damage(HealthBase healthBase)
    {
        
        flashColors.ForEach(i => i.Flash());
        EffectsManager.instance.ChangeVignette();
        ShakeCamera.instance.Shake();
        
    }

    private void OnKill(HealthBase healthBase)
    {
        if (_alive)
        {
            _alive=false;
            animator.SetTrigger("Death");
            colliders.ForEach(i=>i.enabled=false);

            Invoke(nameof(Revive), 3f);
        }
        
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }
    private void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), 0.1f);
    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckpointManager.instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.instance.GetPositionFromLastCheckpoint();
        }
    }

    public void IncraseSize(int value)
    {
        StartCoroutine("IncreaseSizeTime",value);
    }

    IEnumerator IncreaseSizeTime(int value)
    {
        transform.localScale *= value;
        yield return new WaitForSeconds(10);
        transform.localScale = Vector3.one;
    }


    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed,float duration)
    {
        var defaultSpeed = speed;
        speed *= localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup,float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChange.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChange.ApplyDefaultTexture();
    }

    public void ChangeGravity(bool value, float duration)
    {
        //invertGravity = value;
        StartCoroutine(ChangeGravityCoroutine(value, duration));
    }
    public void ApplyGravityChange()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (invertGravity)
            {
                gravity *= -1;
            }
        }
        
    }

    IEnumerator ChangeGravityCoroutine(bool value,float duration)
    {
        invertGravity = value;
        yield return new WaitForSeconds(duration);
        invertGravity = !value;
    }

    
}
