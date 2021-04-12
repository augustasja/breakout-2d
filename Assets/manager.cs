using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<ParticleSystem>().Pause();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.name == "Tilemap")
        {
            GetComponent<ParticleSystem>().Play();
        }
    }

}

