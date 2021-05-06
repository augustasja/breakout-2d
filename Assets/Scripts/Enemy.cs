using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;

    public ScoreManager scoreManager;

    public float speed;
    private Transform target;


    public bool mustPatrol;

    public Rigidbody2D rb;

    private bool mustTurn;

    public Transform groundCheckpos;

    public LayerMask groundLayer;

    private Animator m_animator;

    public LayerMask playerMask;

    public int attackDamage = 50;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public Transform attackPoint;
    public float attackRange = 0.5f;

    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Transform>();
        mustPatrol = true;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
        else if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }
    }

    void Patrol()
    {
        if (mustTurn)
        {
            Flip();
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void Flip()

    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        mustPatrol = true;

    }

    public void Attack()
    {
        m_animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange,
            playerMask);

        foreach (var item in hitEnemies)
        {
            Debug.Log("hit " + item.name);

            item.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died");

        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        scoreManager.totalScore += (int)(maxHealth * 0.05);
        Debug.Log("High Score: " + scoreManager.totalScore);
        this.enabled = false;

    }

}
