using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IInput
{
    private InputActions _inputActions;
    private InputAction _mousePosition, _mouseClick, _mouseScroll;
    public static PlayerInput Instance { get; private set; }
    public Vector3 MousePosition => _mousePosition.ReadValue<Vector2>();

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
        _mouseScroll = _inputActions.GamePlay.MouseScroll;
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
            MouseClick = _mouseClick.WasPressedThisFrame(),
            MouseScroll = _mouseScroll.ReadValue<float>()
        };
    }
}

public struct FrameInput
{
    public Vector2 MousePosition;
    public bool MouseClick;
    public float MouseScroll;
}

public interface IInput
{
    FrameInput GatherInput();
}