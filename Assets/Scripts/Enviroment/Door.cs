using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform iconAnchor;
    [SerializeField] private Transform doorModel;
    [SerializeField] private Vector3 openOffset = new Vector3(3f, 0f, 0f);
    [SerializeField] private float openDuration = 1f;
    [SerializeField] private float openDelay = 1f;

    private bool isOpen;

    public Transform IconAnchor => iconAnchor != null ? iconAnchor : transform;
    public bool CanInteract => !isOpen;

    public void Interact()
    {
        if (isOpen) return;
        isOpen = true;
        StartCoroutine(OpenRoutine());
    }

    private IEnumerator OpenRoutine()
    {
        if (openDelay > 0f)
        {
            yield return new WaitForSeconds(openDelay);
        }

        Vector3 start = doorModel.localPosition;
        Vector3 end = start + openOffset;

        for (float t = 0f; t < 1f; t += Time.deltaTime / openDuration)
        {
            doorModel.localPosition = Vector3.Lerp(start, end, t);
            yield return null;
        }
        doorModel.localPosition = end;
    }
}
