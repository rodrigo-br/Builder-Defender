using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    private BuildingTypeCollectionSO _buildingTypeCollection;
    private BuildingTypeSO _activeBuildingType;
    private IInput _playerInput;
    private FrameInput _frameInput;
    private Camera _mainCamera;

    private void Awake()
    {
        Instance = this;
        _buildingTypeCollection = Resources.Load<BuildingTypeCollectionSO>(typeof(BuildingTypeCollectionSO).Name);
        _activeBuildingType = _buildingTypeCollection.List[0];
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _playerInput = PlayerInput.Instance;
        Debug.Log(_playerInput);
    }

    private void Update()
    {
        _frameInput = _playerInput.GatherInput();
        if (_frameInput.MouseClick && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log(_activeBuildingType);
            Debug.Log(_activeBuildingType.Prefab);
            Debug.Log(_mainCamera);
            Instantiate(_activeBuildingType.Prefab, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        return (_mainCamera.ScreenToWorldPoint(_frameInput.MousePosition));
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        _activeBuildingType = buildingType;
    }
}
