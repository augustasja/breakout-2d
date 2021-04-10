using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Openable : Interactable
{
    public Sprite open;
    public Sprite closed;

    private SpriteRenderer sr;
    private bool isClosed = true;

    public AudioClip chestSound;
    public AudioClip coinSound;

    public int reward;
    
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closed;
    }

    /// <summary>
    /// Interact metodas, kuris keicia skrynios sprite ir padaro ja atidaryta
    /// </summary>
    public override void Interact()
    {
        if (isClosed)
        {
            sr.sprite = open;
            isClosed = !isClosed;

            StartCoroutine(PlaySoundWithDelay(0f, chestSound));
            StartCoroutine(PlaySoundWithDelay(1.5f, coinSound));
            ScoreManager.instance.ChangeScore(reward);
        }
    }

    private IEnumerator PlaySoundWithDelay(float delay, AudioClip audio)
    {
        yield return new WaitForSeconds(delay);
        AudioSource.PlayClipAtPoint(audio, transform.position);
    }
}
