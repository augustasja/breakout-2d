using UnityEngine;

public class manager : MonoBehaviour
{

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

