using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CinemachineInputHandler : MonoBehaviour
{
    [SerializeField] private float lookSensitivity = 0.1f;
    private void Awake()
    {
        CinemachineCore.GetInputAxis = CustomGetInputAxis;
    }
    private float CustomGetInputAxis(string axisName)
    {
        if (Mouse.current == null) return 0f;

        if (Mouse.current.leftButton.isPressed)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return 0f;
            }
            Vector2 mouseDelta = Mouse.current.delta.ReadValue() * lookSensitivity;
            if (axisName == "Mouse X") return mouseDelta.x;
            if (axisName == "Mouse Y") return mouseDelta.y;
        }

        return 0f;
    }
}
