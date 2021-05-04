using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var moveHorizontal = player.transform.position.x - transform.position.x;
        var moveVertical = player.transform.position.y - transform.position.y;

        var movement = new Vector2(moveHorizontal, moveVertical);
        transform.LookAt(player.transform);

        this.transform.position += transform.forward * speed * 0.5f * Time.deltaTime;
    }
}
