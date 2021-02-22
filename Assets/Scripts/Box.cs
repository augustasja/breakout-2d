using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public TextMeshProUGUI text; // Text for displaying actions.

    private bool isTouched; // Bool for storing value if player has touched the box.

    public float fadeOutTime = 1.0f; // Object fade out time

    public SpriteRenderer spriteRenderer; // Sprite that will fade out


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouched)
        {
            StartCoroutine(FadeOut(spriteRenderer));
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
            Debug.Log($"{collision.collider.name} touched " +
                $"{collision.otherCollider.name}");
            Debug.Log($"{collision.otherCollider.name} " +
                $"will self destruct in " +
                $"${TimeSpan.FromSeconds(fadeOutTime)} seconds.");
            text.text = $"{collision.collider.name} touched the box!";
            isTouched = true;
        }
    }

    /// <summary>
    /// Handling event for when the box is touched
    /// </summary>
    /// <param name="sprite">SpriteRender obj, that will disappear</param>
    /// <returns></returns>
    private IEnumerator FadeOut(SpriteRenderer sprite)
    {
        var tempColor = sprite.color;
        while(tempColor.a > 0f)
        {
            tempColor.a -= 1f * Time.deltaTime / fadeOutTime;
            sprite.color = tempColor;
            if (tempColor.a <= 0f) tempColor.a = 0f;
            yield return null;
        }
        Destroy(this.gameObject);
        sprite.color = tempColor;

    }
}
