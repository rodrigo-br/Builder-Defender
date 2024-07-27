using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float _minOrthographicSize = 10f;
    [SerializeField] private float _maxOrthographicSize = 30f;
    [SerializeField] private float _zoomSpeed = 5f;
    private CinemachineVirtualCamera _virtualCamera;
    private IInput _playerInput;
    private FrameInput _frameInput;
    private Camera _mainCamera;
    private float _targetOrthographicSize;
    private float _orthographicSize;

    private void Awake()
    {
        _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _playerInput = PlayerInput.Instance;
        _mainCamera = Camera.main;
        _orthographicSize = _virtualCamera.m_Lens.OrthographicSize;
        _targetOrthographicSize = _orthographicSize;
    }

    private void Update()
    {
        _frameInput = _playerInput.GatherInput();
        if (_frameInput.MouseScroll != 0)
        {
            _targetOrthographicSize = Mathf.Clamp(_orthographicSize - _frameInput.MouseScroll, _minOrthographicSize, _maxOrthographicSize);
        }
        if (Mathf.Abs(_targetOrthographicSize - _orthographicSize) >= 0.01f)
        {
            _orthographicSize = Mathf.Lerp(_orthographicSize, _targetOrthographicSize, Time.deltaTime * _zoomSpeed);
            _virtualCamera.m_Lens.OrthographicSize = _orthographicSize;
        }
    }
}
