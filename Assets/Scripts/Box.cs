using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Box : MonoBehaviour
{
    public TextMeshProUGUI Text; // Text for displaying actions.

    private bool _isTouched; // Bool for storing value if player has touched the box.

    public float FadeOutTime = 1.0f; // Object fade out time

    public SpriteRenderer SpriteRenderer; // Sprite that will fade out


    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTouched)
        {
            StartCoroutine(FadeOut(SpriteRenderer));
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
                $"${TimeSpan.FromSeconds(FadeOutTime)} seconds.");
            Text.text = $"{collision.collider.name} touched the box!";
            _isTouched = true;
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
        while (tempColor.a > 0f)
        {
            tempColor.a -= 1f * Time.deltaTime / FadeOutTime;
            sprite.color = tempColor;
            if (tempColor.a <= 0f) tempColor.a = 0f;
            yield return null;
        }
        Destroy(gameObject);
        sprite.color = tempColor;

    }
}
