using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float _minOrthographicSize = 10f;
    [SerializeField] private float _maxOrthographicSize = 30f;
    private CinemachineVirtualCamera _virtualCamera;
    private IInput _playerInput;
    private FrameInput _frameInput;
    private Camera _mainCamera;

    private void Awake()
    {
        _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _playerInput = PlayerInput.Instance;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _frameInput = _playerInput.GatherInput();
        if (_frameInput.MouseScroll != 0)
        {
            float orthographicSize = _virtualCamera.m_Lens.OrthographicSize - _frameInput.MouseScroll;
            orthographicSize = Mathf.Clamp(orthographicSize, _minOrthographicSize, _maxOrthographicSize);
            _virtualCamera.m_Lens.OrthographicSize = orthographicSize;
        }
    }
}
