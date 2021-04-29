using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Openable : Interactable
{
    public Sprite Open;
    public Sprite Closed;

    private SpriteRenderer _spriteRenderer;
    private bool _isClosed = true;

    public AudioClip ChestSound;
    public AudioClip CoinSound;

    public int Reward;
    public ScoreManager ScoreManager;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = Closed;
    }

    /// <summary>
    /// Interact metodas, kuris keicia skrynios sprite ir padaro ja atidaryta
    /// </summary>
    public override void Interact()
    {
        if (_isClosed)
        {
            _spriteRenderer.sprite = Open;
            _isClosed = !_isClosed;

            StartCoroutine(PlaySoundWithDelay(0f, ChestSound));
            StartCoroutine(PlaySoundWithDelay(1.5f, CoinSound));
            ScoreManager.ChangeScore(Reward, "+");
            
            
            
        }
    }

    private IEnumerator PlaySoundWithDelay(float delay, AudioClip audio)
    {
        yield return new WaitForSeconds(delay);
        AudioSource.PlayClipAtPoint(audio, transform.position);
    }
}
