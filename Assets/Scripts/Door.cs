using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Door : Interactable
{
    public Sprite Open;
    public Sprite Closed;

    public bool openedDoors;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = Closed;
    }

    public override void Interact()
    {
        if (openedDoors)
        {
            Debug.Log("Baigiamas lygis"); // Cia lygio pabaigos message
            openedDoors = !openedDoors;
        }
    }

}
