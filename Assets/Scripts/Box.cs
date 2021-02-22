using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public TextMeshProUGUI text; // Text for displaying actions.

    private bool isTouched; // Bool for storing value if player has touched the box.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouched)
        {
            StartCoroutine(Handle());
        }
    }

    /// <summary>
    /// Collision event that checks wheter the player has touched the gameObject
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            UnityEngine.Debug.Log($"{collision.collider.name} touched {collision.otherCollider.name}");
            text.text = $"{collision.collider.name} touched {collision.otherCollider.name}!";
            isTouched = true;
        }
    }

    /// <summary>
    /// Handling event for when the box is touched
    /// </summary>
    /// <returns></returns>
    private IEnumerator Handle()
    {
        isTouched = true;
        yield return new WaitForSeconds(1.0f);
        UnityEngine.Debug.Log($"Destroyed: {gameObject.name}");
        Destroy(gameObject);
        isTouched = false;
    }
}
