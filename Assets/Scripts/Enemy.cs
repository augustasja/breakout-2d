using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;

    public ScoreManager scoreManager;

    void Start()
    {
        currentHealth = maxHealth;
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
