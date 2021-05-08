using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health, Score")]
    public int MaxHealth = 100;
    public int CurrentHealth;
    public ScoreManager Score;

    [Header("Animations, Movement")]
    public Animator Animator;
    public float Speed;

    [Header("Attack")]
    public int AttackDamage = 50;
    public float AttackRate = 2f;
    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask PlayerMask;
    private Transform _target;
    public float GapToTarget = 1f;
    private float _nextAttackTime = 0f;
    public float TriggerDistance = 0f;
    private float _lastTimeSeen;

    private Vector3 CharacterScale;
    private float CharacterScaleX;

    void Start()
    {
        CurrentHealth = MaxHealth;

        // Declare the target.
        _target = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Transform>();

        // Character scale for turning the character sprite.
        CharacterScale = transform.localScale;
        CharacterScaleX = CharacterScale.x;
    }

    private void Update()
    {
        // Make the enemies move towards the player object.
        var currentGapToTarget = Mathf.Abs(transform.position.x -
            _target.position.x);
        if (currentGapToTarget > GapToTarget 
            && currentGapToTarget <= TriggerDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                _target.position, Speed * Time.deltaTime);
            Animator.SetInteger("AnimState", 2);
            _lastTimeSeen = Time.time % 60;
        }
        else if(currentGapToTarget <= GapToTarget && !Player.m_isDead)
        {
            if (Time.time >= _nextAttackTime )
            {
                Attack();
                _nextAttackTime = Time.time + 1f / AttackRate;
            }
            Animator.SetInteger("AnimState", 1);
        }
        else if((Time.time - _lastTimeSeen) >= 5f || Player.m_isDead ||
             currentGapToTarget > GapToTarget)
        {
            Animator.SetInteger("AnimState", 0);
        }

    }

    void FixedUpdate()
    {
        // Flipping enemy sprite.
        if (transform.position.x < _target.position.x)
            CharacterScale.x = -CharacterScaleX;
        else
            CharacterScale.x = CharacterScaleX;

        transform.localScale = CharacterScale;
    }

    /// <summary>
    /// Attack all the colliders who overlap within the AttackPoint range.
    /// </summary>
    public void Attack()
    {
        Animator.SetTrigger("Attack");

        Collider2D[] hitEnemies =
            Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange,
            PlayerMask);

        // Damage every enemy hit.
        foreach (var enemy in hitEnemies)
        {
            Debug.Log("Hit: " + enemy.name);
            enemy.GetComponent<Player>().TakeDamage();
        }
    }

    /// <summary>
    /// Draw a circle around the attack point to display its reach.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }


    public void TakeDamage(int damage)
    {
        Animator.SetTrigger("Hurt");
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            Die();
    }

    public void Die()
    {
        Animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        Score.totalScore += (int)(MaxHealth * 0.05);
        enabled = false;

        Debug.Log("Enemy died.");
        Debug.Log("High Score: " + Score.totalScore);
    }

}
