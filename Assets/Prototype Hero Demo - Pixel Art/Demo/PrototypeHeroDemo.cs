using System;
using UnityEngine;
using System.Collections;

public class PrototypeHeroDemo : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] float      m_maxSpeed = 4.5f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] bool       m_hideSword = false;
    [Header("Effects")]
    [SerializeField] GameObject m_RunStopDust;
    [SerializeField] GameObject m_JumpDust;
    [SerializeField] GameObject m_LandingDust;
    
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Prototype    m_groundSensor;
    private AudioSource         m_audioSource;
    private AudioManager_PrototypeHero m_audioManager;
    public AudioManager _audioManager;
    private bool                m_grounded = false;
    private bool                m_moving = false;
    private int                 m_facingDirection = 1;
    private float               m_disableMovementTimer = 0.0f;
    private bool                m_inAir;
    private float               m_scoreMultiplier = 1;


    public ParticleSystem dust;

    public ScoreManager scoreManager;

    public Pause pauseManager;


    // Interaktable svirtis
    [SerializeField] GameObject interactIcon; // letter E
    private Vector2 boxSize = new Vector2(0.1f,1f);

    // Use this for initialization
    void Start ()
    {
        interactIcon.SetActive(false);
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
        m_audioManager = AudioManager_PrototypeHero.instance;
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
        dust.Pause();
    }

    // Update is called once per frame
    void Update ()
    {
        // Decrease timer that disables input movement. Used when attacking
        m_disableMovementTimer -= Time.deltaTime;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = 0.0f;

        if (m_disableMovementTimer < 0.0f)
            inputX = Input.GetAxis("Horizontal");

        // GetAxisRaw returns either -1, 0 or 1
        float inputRaw = Input.GetAxisRaw("Horizontal");
        // Check if current move input is larger than 0 and the move direction is equal to the characters facing direction
        if (Mathf.Abs(inputRaw) > Mathf.Epsilon && Mathf.Sign(inputRaw) == m_facingDirection)
            m_moving = true;

        else
            m_moving = false;

        // Swap direction of sprite depending on move direction
        if (inputRaw > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputRaw < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }
     
        // SlowDownSpeed helps decelerate the characters when stopping
        float SlowDownSpeed = m_moving ? 1.0f : 0.5f;
        // Set movement
        m_body2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);

        // Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // Set Animation layer for hiding sword
        int boolInt = m_hideSword ? 0 : 1;
        m_animator.SetLayerWeight(1, boolInt);

        // -- Handle Animations --
        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            // Checking if player is grounded.
            if (m_grounded && m_grounded && m_disableMovementTimer < 0.0f)
            {
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
                m_inAir = true;

            }
            // Check if player is able to perform an extra jump for 1 "coin".
            else if (m_inAir && scoreManager.Score > 0)
            {
                // Burning score for an extra jump.
                var scoreToBurn = 1;
                scoreManager.ChangeScore(scoreToBurn, "-");
                Debug.Log($"Coins burned: {scoreToBurn}");
                // Performing jumping animations.
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
                m_inAir = true;
            }
        }

        //Run
        else if (m_moving)
        {
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            m_animator.SetInteger("AnimState", 0);
        }
        // Tikriname ar paspaustas interaction mygtukas (E)
        if (Input.GetKeyDown(KeyCode.E))
            CheckInteraction();
    }

    // Function used to spawn a dust effect
    // All dust effects spawns on the floor
    // dustXoffset controls how far from the player the effects spawns.
    // Default dustXoffset is zero
    void SpawnDustEffect(GameObject dust, float dustXOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * m_facingDirection, 0.0f, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(m_facingDirection, 1, 1);
        }
    }

    // Animation Events
    // These functions are called inside the animation files
    void AE_runStop()
    {
        AudioSource.PlayClipAtPoint(_audioManager.FootStep2, transform.position);
        // Spawn Dust
        float dustXOffset = 0.6f;
        SpawnDustEffect(m_RunStopDust, dustXOffset);
    }

    void AE_footstep()
    {
        AudioSource.PlayClipAtPoint(_audioManager.FootStep1, transform.position);
    }

    void AE_Jump()
    {
        AudioSource.PlayClipAtPoint(_audioManager.Jump, transform.position);
        // Spawn Dust
        SpawnDustEffect(m_JumpDust);
    }

    void AE_Landing()
    {
        AudioSource.PlayClipAtPoint(_audioManager.Land, transform.position);
        // Spawn Dust
        SpawnDustEffect(m_LandingDust);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Naikina paimta coin
        if (other.gameObject.CompareTag("GoldenCoin"))
        {
            AudioSource.PlayClipAtPoint(_audioManager.Coin, transform.position);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Slippery Ground")
        {
            dust.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Slippery Ground")
        {
            dust.Pause();
        }
    }

    /// <summary>
    /// Nustatoma interactable icon busena i true
    /// </summary>
    public void OpenInteractableIcon()
    {
        interactIcon.SetActive(true);
    }

    /// <summary>
    /// Nustatoma interactable icon busena i false
    /// </summary>
    public void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }

    /// <summary>
    /// Metodas tikrina ar Raycast ribose yra aptinkamas objektas
    /// jei taip - interactinama su tuo objektu
    /// </summary>
    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    rc.transform.GetComponent<Interactable>().Colided = true;
                    return;
                }

            }
        }
    }
}
