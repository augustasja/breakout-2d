using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    public static bool m_isDead = false;
    public AudioManager _audioManager;
    private bool m_inAir = false;
    public ScoreManager scoreManager;
    public TextMeshProUGUI healthText;

    // Interaktable svirtis
    [SerializeField] GameObject interactIcon; // letter E
    private Vector2 boxSize = new Vector2(0.1f, 1f);

    [Header("Player state")]
    [Min(0)]
    [SerializeField]
    private int lives = 3;

    [Header("Events")]
    [SerializeField]
    private UnityEvent<int> onUpdateLives;

    [SerializeField]
    private UnityEvent onStopGame;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        InvokeOnUpdateLives();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
            _audioManager.PlaySound(_audioManager.Land, transform.position);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("r"))
        {
            if (!m_isDead)
            {
                m_animator.SetTrigger("Death");
                _audioManager.PlaySound(_audioManager.Death, transform.position);
            }
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }

        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack
        else if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Attack");
            _audioManager.PlaySound(_audioManager.Sword, transform.position);
        }

        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        //Jump
        //else if (Input.GetKeyDown("space") && m_grounded)
        //{
        //    m_animator.SetTrigger("Jump");
        //    m_grounded = false;
        //    m_animator.SetBool("Grounded", m_grounded);
        //    m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        //    m_groundSensor.Disable(0.2f);
        //}

        else if (Input.GetKeyDown("space"))
        {
            if (m_grounded)
            {
                _audioManager.PlaySound(_audioManager.Jump, transform.position);
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
                m_inAir = true;
            }
            else if (m_inAir && scoreManager.Score > 0)
            {
                _audioManager.PlaySound(_audioManager.Jump, transform.position);
                // Burning score for an extra jump.
                var scoreToBurn = 1;
                TakeDamage();

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
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            m_animator.SetInteger("AnimState", 2);
        }

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);

        // Tikriname ar paspaustas interaction mygtukas (E)
        if (Input.GetKeyDown(KeyCode.E))
            CheckInteraction();
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
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position,
            boxSize, 0, Vector2.zero);
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

    public void TakeDamage()
    {
        Debug.Log("Taken damage");
        AddLives(-1);
        healthText.text = "x" + lives;
        if (lives <= 0)
        {
            m_animator.SetTrigger("Death");
            _audioManager.PlaySound(_audioManager.Death, transform.position);
            onStopGame.Invoke();
        }
    }

    public void AddLives(int x)
    {
        lives += x;
    }

    private void InvokeOnUpdateLives()
    {
        onUpdateLives.Invoke(lives);
    }
}
