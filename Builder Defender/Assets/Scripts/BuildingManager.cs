using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Transform _WoodTowerPrefab;
    private IInput _playerInput;
    private FrameInput _frameInput;
    private Camera _mainCamera;

    private void Awake()
    {
        _playerInput = GetComponent<IInput>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _frameInput = _playerInput.GatherInput();
        if (_frameInput.MouseClick)
        {
            Instantiate(_WoodTowerPrefab, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        return (_mainCamera.ScreenToWorldPoint(_frameInput.MousePosition));
    }
}
