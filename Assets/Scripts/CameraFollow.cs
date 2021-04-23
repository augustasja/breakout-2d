using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform Player;
    public float Distance = 30.0f;

    void Awake()
    {
        GetComponent<Camera>().orthographicSize = ((Screen.height / 2) / Distance);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
    }
}
