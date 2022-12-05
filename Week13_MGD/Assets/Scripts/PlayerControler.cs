using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    public float hForce;
    public float hSpeed;
    public float vForce;
    public float airControl;
    public Transform groundDetect;
    public float groundRadius;
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("Animations")]
    public Animator animator;
    public PlayerAnimationState playerAnimationState;

    [Header("Particle Effects")]
    public ParticleSystem dustTrail;
    public Color dustTrailColor;

    [Header("Screen Shake")]
    public CinemachineVirtualCamera playerCamera;
    public CinemachineBasicMultiChannelPerlin perlin;
    public float shakeIntensity;
    public float shakeDuration;
    public float shakeTimer;
    public bool isCameraShaking;

    [Header("Health System")]
    public HealthBarController health;
    public Lives life;
    public DeathPlane deathPlane;

    [Header("Controls")]
    public Joystick joystick;

    public Rigidbody2D rigidbody;
    public SoundManager soundManager;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        joystick = (Application.isMobilePlatform) ? GameObject.Find("Joystick").GetComponent<Joystick>() : null;
        FindObjectOfType<PlayerHealth>().GetComponent<HealthBarController>();
        life = FindObjectOfType<Lives>();
        deathPlane = FindObjectOfType<DeathPlane>();

        soundManager = FindObjectOfType<SoundManager>();

        dustTrail = GetComponentInChildren<ParticleSystem>();

        playerCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        perlin = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        isCameraShaking = false;
        shakeTimer = shakeDuration;
    }

    private void Update()
    {
        if (health.value <= 0)
        {
            life.LoseALife();
            if (life.value > 0)
            {
                health.ResetHealth();
                deathPlane.Respawn(gameObject);
                soundManager.PlaySoundFX(SoundEffects.DEATH, Channel.PLAYER_DEATH_FX);
            }
            else if (life.value <= 0)
            {
                SceneManager.LoadScene(1);
            }
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Ground check
        var hit = Physics2D.OverlapCircle(groundDetect.position, groundRadius, groundLayer);
        isGrounded = hit;
        if (Input.GetKeyDown(KeyCode.K))
        {
            health.TakeDamage(100);
        }

        Move();
        Jump();

        //Camera shake control
        if (isCameraShaking)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0.0f) //timed out
            {
                perlin.m_AmplitudeGain = 0.0f;
                shakeTimer = shakeDuration;
                isCameraShaking = false;
            }
        }

    }

    private void Move()
    {
        var x = Input.GetAxisRaw("Horizontal") + ((Application.isMobilePlatform) ? joystick.Horizontal : 0.0f);

        if (x != 0.0f)
        {
            x = (x > 0.0f) ? 1.0f : -1.0f;

            rigidbody.AddForce(Vector2.right * x * hForce * ((isGrounded) ? 1.0f : airControl));

            rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, hSpeed);

            if(isGrounded)
            {
                CreateDustTrail();
            }
        }

        if (x < 0.0f)
        {
            ChangeAnimation(PlayerAnimationState.RUNL);
        }
        else if (x > 0.0f)
        {
            ChangeAnimation(PlayerAnimationState.RUNR);
        }

        if (isGrounded && x == 0.0f)
        {
            ChangeAnimation(PlayerAnimationState.IDLE);
        }

        
    }

    private void CreateDustTrail()
    {
        dustTrail.GetComponent<Renderer>().material.SetColor("_Color", dustTrailColor);
        dustTrail.Play();
    }

    private void ShakeCamera()
    {
        perlin.m_AmplitudeGain = shakeIntensity;
        isCameraShaking = true;
    }

    public void Jump()
    {
        var y = Input.GetAxis("Jump");

        if ((isGrounded) && (y > 0.0))
        {
            rigidbody.AddForce(Vector2.up * vForce, ForceMode2D.Impulse);
            soundManager.PlaySoundFX(SoundEffects.JUMP, Channel.PLAYER_SOUND_FX);
        }
    }

    public void JumpButton()
    {
        if (isGrounded)
        {
            rigidbody.AddForce(Vector2.up * vForce, ForceMode2D.Impulse);
            soundManager.PlaySoundFX(SoundEffects.JUMP, Channel.PLAYER_SOUND_FX);
        }
    }

    private void ChangeAnimation(PlayerAnimationState animationState)
    {
        playerAnimationState = animationState;
        animator.SetInteger("AnimationState", (int)playerAnimationState);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundDetect.position, groundRadius); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health.TakeDamage(10);
            soundManager.PlaySoundFX(SoundEffects.HURT, Channel.PLAYER_HURT_FX);

            //Play Took damage animation
            ShakeCamera();
        }
    }


}
