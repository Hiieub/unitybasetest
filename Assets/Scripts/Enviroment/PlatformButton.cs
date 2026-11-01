using System;
using UnityEngine;

public class PlatformButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform iconAnchor;
    public event Action OnPressed;

    private bool pressed;

    public Transform IconAnchor => iconAnchor != null ? iconAnchor : transform;
    public bool CanInteract => !pressed;

    public void Interact()
    {
        if (pressed) return;
        pressed = true;
        OnPressed?.Invoke();
    }
}