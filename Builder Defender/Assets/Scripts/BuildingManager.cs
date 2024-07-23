using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private BuildingTypeCollectionSO _buildingTypeCollection;
    private BuildingTypeSO _buildingType;
    private IInput _playerInput;
    private FrameInput _frameInput;
    private Camera _mainCamera;
    private int _currentIndex;

    private void Awake()
    {
        _buildingTypeCollection = Resources.Load<BuildingTypeCollectionSO>(typeof(BuildingTypeCollectionSO).Name);
        _currentIndex = 0;
        _buildingType = _buildingTypeCollection.List[_currentIndex];
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _playerInput = PlayerInput.Instance;
    }

    private void Update()
    {
        _frameInput = _playerInput.GatherInput();
        if (_frameInput.MouseClick)
        {
            Instantiate(_buildingType.Prefab, GetMouseWorldPosition(), Quaternion.identity);
            _currentIndex = (_currentIndex + 1) % _buildingTypeCollection.List.Count;
            _buildingType = _buildingTypeCollection.List[_currentIndex];
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        return (_mainCamera.ScreenToWorldPoint(_frameInput.MousePosition));
    }
}
