using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IInput
{
    private InputActions _inputActions;
    private InputAction _mousePosition, _mouseClick;
    public static PlayerInput Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        _inputActions = new InputActions();
        _mousePosition = _inputActions.GamePlay.MousePosition;
        _mouseClick = _inputActions.GamePlay.MouseClick;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    public FrameInput GatherInput()
    {
        return new FrameInput
        {
            MousePosition = _mousePosition.ReadValue<Vector2>(),
            MouseClick = _mouseClick.WasPressedThisFrame()
        };
    }
}

public struct FrameInput
{
    public Vector2 MousePosition;
    public bool MouseClick;
}

public interface IInput
{
    FrameInput GatherInput();
}