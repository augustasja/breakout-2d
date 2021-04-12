using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slip : MonoBehaviour
{

    public ParticleSystem dust;
    // Start is called before the first frame update
    void Start()
    {
        dust.Pause();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vetical");
            var vector = new Vector2(x, y);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = 
                vector * 5000f * Time.deltaTime;
            dust.Play();
        }
    }
}
