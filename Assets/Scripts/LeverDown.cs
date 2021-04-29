using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LeverDown : Interactable
{
    
    public Sprite Up;
    public Sprite Down;
    
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _doorSprite;
    private GameObject openDoor;
    public bool _isUp = true;

    public AudioClip LeverSound;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _doorSprite = GameObject.Find("ClosedDoor").GetComponent<SpriteRenderer>(); 
        openDoor = GameObject.Find("ClosedDoor");
        _spriteRenderer.sprite = Up;
    }

    /// <summary>
    /// Interact metodas, kuris keicia skrynios sprite ir padaro ja atidaryta
    /// </summary>
    public override void Interact()
    {
        if (_isUp)
        {
            _spriteRenderer.sprite = Down;
            _isUp = !_isUp;
            StartCoroutine(PlaySoundWithDelay(0f, LeverSound));
            _doorSprite.sprite = openDoor.GetComponent<Door>().Open;
            openDoor.GetComponent<Door>().openedDoors = true;
            openDoor.GetComponent<Door>().GetComponent<Interactable>().Colided = false;
        }
    }

    private IEnumerator PlaySoundWithDelay(float delay, AudioClip audio)
    {
        yield return new WaitForSeconds(delay);
        AudioSource.PlayClipAtPoint(audio, transform.position);
    }
}
