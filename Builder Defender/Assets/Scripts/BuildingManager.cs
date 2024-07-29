using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;
    public FrameInput FrameInput { get; private set; }
    private BuildingTypeSO _activeBuildingType;
    private IInput _playerInput;
    private bool _canPlaceBuilding = true;

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
        _playerInput = PlayerInput.Instance;
        Debug.Log(_playerInput);
    }

    private void Update()
    {
        FrameInput = _playerInput.GatherInput();
        if (FrameInput.MouseClick && !EventSystem.current.IsPointerOverGameObject() && _activeBuildingType != null && _canPlaceBuilding)
        {
            Instantiate(_activeBuildingType.Prefab, Utils.GetMouseWorldPosition(), Quaternion.identity);
            SetActiveBuildingType(null);
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        _activeBuildingType = buildingType;
        OnActiveBuildingTypeChangedEventArgs onActiveBuildingTypeChangedEventArgs = new();
        if (buildingType != null)
        {
            onActiveBuildingTypeChangedEventArgs.Sprite = buildingType.Sprite;
            onActiveBuildingTypeChangedEventArgs.ResourceDetectionRadius = buildingType.ResourceGeneratorData.ResourceDetectionRadius;
            onActiveBuildingTypeChangedEventArgs.BuildingType = buildingType;
        }
        OnActiveBuildingTypeChanged?.Invoke(this, onActiveBuildingTypeChangedEventArgs);
    }

    public void SetCanPlaceBuilding(bool value)
    {
        _canPlaceBuilding = value;
    }

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public Sprite Sprite;
        public float ResourceDetectionRadius;
        public BuildingTypeSO BuildingType;
    }
}
