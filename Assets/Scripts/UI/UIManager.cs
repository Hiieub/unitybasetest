using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Button btnReset;

    [SerializeField] private TextMeshProUGUI txtTitle;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (btnReset != null) btnReset.onClick.AddListener(HandleBtnClicked);

        if (txtTitle != null) txtTitle.gameObject.SetActive(false);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerDied += GameManager_OnPlayerDied;
            GameManager.Instance.OnGameCompleted += GameManager_OnGameCompleted;
        }
    }

    private void OnDestroy()
    {
        if (btnReset != null) btnReset.onClick.RemoveListener(HandleBtnClicked);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerDied -= GameManager_OnPlayerDied;
            GameManager.Instance.OnGameCompleted -= GameManager_OnGameCompleted;
        }
    }

    private void GameManager_OnGameCompleted()
    {
        ShowTitle("Complete");
    }

    private void GameManager_OnPlayerDied()
    {
        ShowTitle("Died");
    }

    public void ShowTitle(string message)
    {
        if (txtTitle != null)
        {
            txtTitle.text = message;
            txtTitle.gameObject.SetActive(true);
        }
    }

    private void HandleBtnClicked()
    {
        if (GameManager.Instance != null) GameManager.Instance.ResetGame();
    }
}
