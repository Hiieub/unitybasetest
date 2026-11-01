using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController _))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetPlayerDied();
            }
        }
    }
}
