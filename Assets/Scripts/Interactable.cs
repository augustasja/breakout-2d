using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    public bool isColided = false;
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public abstract void Interact();

    /// <summary>
    /// Metodas tikrina ar zaidejas yra netoli interactable objekto
    /// jei taip - atisranda interactable icon
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColided != true)
        {
            if(collision.CompareTag("Player"))
                collision.GetComponent<PrototypeHeroDemo>().OpenInteractableIcon();
        }
    }

    /// <summary>
    /// Metodas tikrina ar zaidejas isejo is interactable objekto ribu
    /// jei taip - pranyksta interactable icon
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            collision.GetComponent<PrototypeHeroDemo>().CloseInteractableIcon();
    }
}
