using UnityEngine;

public static class Utils
{
    private static Camera _mainCamera;

    public static Vector3 GetMouseWorldPosition()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
        Vector2 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(PlayerInput.Instance.MousePosition);
        return mouseWorldPosition;
    }
}
