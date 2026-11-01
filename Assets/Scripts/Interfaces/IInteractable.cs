using UnityEngine;

public interface IInteractable
{
    Transform IconAnchor { get; }
    bool CanInteract {  get; }
    void Interact();
}
