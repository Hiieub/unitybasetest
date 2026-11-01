using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private readonly List<IInteractable> nearby = new List<IInteractable>();

    public IInteractable Current { get; private set; }
    public event Action<IInteractable> OnCurrentChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && !nearby.Contains(interactable))
            nearby.Add(interactable);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
            nearby.Remove(interactable);
    }

    private void Update()
    {
        IInteractable best = null;
        float bestDist = float.MaxValue;

        foreach (IInteractable item in nearby)
        {
            if (!item.CanInteract) continue;

            float dist = (item.IconAnchor.position - transform.position).sqrMagnitude;
            if (dist < bestDist)
            {
                bestDist = dist;
                best = item;
            }
        }

        if (best != Current)
        {
            Current = best;
            OnCurrentChanged?.Invoke(Current);
        }
    }
}