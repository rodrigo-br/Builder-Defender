using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private float _moveSpeed;
    private IInput _playerInput;
    private FrameInput _frameInput;
    private Camera _mainCamera;
    private Vector3 _destination;

    private void Awake()
    {
        _destination = transform.position;
    }

    private void Start()
    {
        _playerInput = PlayerInput.Instance;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _frameInput = _playerInput.GatherInput();
        if (_frameInput.MouseClick)
        {
            MouseClick();
        }
        if (Vector3.Distance(transform.position, _destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destination, _moveSpeed * Time.deltaTime);
        }
    }

    private void MouseClick()
    {
        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(_frameInput.MousePosition);
        Vector3Int gridPosition = _tilemap.WorldToCell(mousePosition);
        if (_tilemap.HasTile(gridPosition))
        {
            _destination = mousePosition;
        }
    }
}
