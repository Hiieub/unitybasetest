using System.Collections;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private PlatformButton platformButton;
    [SerializeField] private Transform[] platforms;

    [SerializeField] private Vector3 hiddenOffset = new Vector3(0f, 0f, -1.8f);
    [SerializeField] private float extendDuration = 0.5f;

    private Vector3[] targetPos;
    private Vector3[] hiddenPos;
    private bool isExtended;

    private void Awake()
    {
        targetPos = new Vector3[platforms.Length];
        hiddenPos = new Vector3[platforms.Length];

        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i] != null)
            {
                targetPos[i] = platforms[i].localPosition;
                hiddenPos[i] = targetPos[i] + hiddenOffset;

                platforms[i].localPosition = hiddenPos[i];
            }
        }
    }

    private void Start()
    {
        if (platformButton != null)
        {
            platformButton.OnPressed += ExtendPlatforms;
        }
    }
    private void OnDestroy()
    {
        if (platformButton != null)
        {
            platformButton.OnPressed -= ExtendPlatforms;
        }
    }
    private void ExtendPlatforms()
    {
        if (isExtended) return;
        isExtended = true;
        StartCoroutine(ExtendAllRoutine());
    }

    private IEnumerator ExtendAllRoutine()
    {
        float elapsed = 0f;
        while (elapsed < extendDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / extendDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            for (int i = 0; i < platforms.Length; i++)
            {
                if (platforms[i] != null)
                {
                    platforms[i].localPosition = Vector3.Lerp(hiddenPos[i], targetPos[i], smoothT);
                }
            }
            yield return null;
        }

        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i] != null)
            {
                platforms[i].localPosition = targetPos[i];
            }
        }
    }
}
