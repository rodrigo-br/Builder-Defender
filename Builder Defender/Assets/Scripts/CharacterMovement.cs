using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private float _moveSpeed;
    public float Direction { get; private set; }
    private IInput _playerInput;
    private FrameInput _frameInput;
    private Vector3 _destination;

    private void Awake()
    {
        _destination = transform.position;
    }

    private void Start()
    {
        _playerInput = PlayerInput.Instance;
    }

    private void Update()
    {
        _frameInput = _playerInput.GatherInput();
        if (_frameInput.MouseClick && !EventSystem.current.IsPointerOverGameObject())
        {
            MouseClick();
        }
        if (Vector3.Distance(transform.position, _destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destination, _moveSpeed * Time.deltaTime);
            Direction = GetDirection(_destination - transform.position);
        }
        else
        {
            Direction = -1;
        }
    }

    private void MouseClick()
    {
        Vector2 mousePosition = Utils.GetMouseWorldPosition();
        Vector3Int gridPosition = _tilemap.WorldToCell(mousePosition);
        if (_tilemap.HasTile(gridPosition))
        {
            _destination = mousePosition;
        }
    }

    private float GetDirection(Vector3 moveDirection)
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;

        return angle;
    }
}
