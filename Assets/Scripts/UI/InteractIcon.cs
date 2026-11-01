using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractIcon : MonoBehaviour
{
    public static InteractIcon Instance { get; private set; }

    [SerializeField] private InteractionDetector detector;

    [SerializeField] private Button button;
    [SerializeField] private Vector3 screenOffset = new Vector3(0f, 50f, 0f);

    private IInteractable currentTarget;
    private Camera cam;

    public event Action OnInteract;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        cam = Camera.main;

        button.onClick.AddListener(OnButtonClicked);
        button.gameObject.SetActive(false);
    }

    private void Start()
    {
        if (detector != null)
        {
            detector.OnCurrentChanged += InteractionDetector_OnCurrentChanged;
        }
    }

    private void OnDestroy()
    {
        if (detector != null)
        {
            detector.OnCurrentChanged -= InteractionDetector_OnCurrentChanged;
        }
    }

    private void InteractionDetector_OnCurrentChanged(IInteractable newTarget)
    {
        currentTarget = newTarget;
        button.gameObject.SetActive(currentTarget != null);
    }

    private void LateUpdate()
    {
        if (currentTarget == null) return;

        Vector3 screenPos = cam.WorldToScreenPoint(currentTarget.IconAnchor.position);
        button.gameObject.SetActive(screenPos.z > 0f);
        button.transform.position = screenPos + screenOffset;
    }

    private void OnButtonClicked()
    {
        OnInteract?.Invoke();

        if (currentTarget != null && currentTarget.CanInteract)
        {
            currentTarget.Interact();

            button.gameObject.SetActive(false);
            currentTarget = null;
        }
    }
}