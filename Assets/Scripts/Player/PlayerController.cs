using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;

    [SerializeField] private Transform mainCamera;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSmoothTime = 0.1f;

    [SerializeField] private float jumpHeght = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    private Vector3 verticalVelocity;

    private float turnSmoothVelocity;

    private PlayerAnimator animator;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<PlayerAnimator>();
    }

    private void Start()
    {
        gameInput.OnJump += GameInput_OnJump;

        if (InteractIcon.Instance != null ) InteractIcon.Instance.OnInteract += InteractIcon_OnInteract;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerDied += GameManager_OnPlayerDied;
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        }
            
    }

    private void OnDestroy()
    {
        gameInput.OnJump -= GameInput_OnJump;

        if (InteractIcon.Instance != null) InteractIcon.Instance.OnInteract -= InteractIcon_OnInteract;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerDied -= GameManager_OnPlayerDied;
            GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
        }
    }

    private void GameManager_OnStateChanged(GameState state)
    {
        if (state != GameState.Playing)
        {
            enabled = false;

            if (animator != null)
            {
                animator.SetRunning(false);
            }
        }
    }

    private void GameInput_OnJump()
    {
        if (!enabled) return;

        if (characterController.isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeght * -2f * gravity);

            if (animator != null)
            {
                animator.PlayJump();
            }
        }
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (gameInput == null) return;

        Vector2 input = gameInput.GetMovement();
        Vector3 moveDir = new Vector3(input.x, 0f, input.y);

        bool isRunning = moveDir.sqrMagnitude > 0.01f;
        Vector3 horizontal = Vector3.zero;

        if (isRunning)
        {
            if (mainCamera == null && Camera.main != null) mainCamera = Camera.main.transform;
            float cameraYaw = mainCamera != null ? mainCamera.eulerAngles.y : 0f;

            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + cameraYaw;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                targetAngle,
                ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 targetDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            horizontal = targetDirection * moveSpeed;
        }     

        if (characterController.isGrounded && verticalVelocity.y < 0f)
            verticalVelocity.y = -2f;

        verticalVelocity.y += gravity * Time.deltaTime;

        characterController.Move((horizontal + verticalVelocity) * Time.deltaTime);

        if (animator != null)
        {
            animator.SetRunning(isRunning);
            animator.SetGrounded(characterController.isGrounded);
        }
    }

    private void InteractIcon_OnInteract()
    {
        animator.PlayInteract();
    }

    private void GameManager_OnPlayerDied()
    {
        animator.PlayDeath();
    }
}
