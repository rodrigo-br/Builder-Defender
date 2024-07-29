using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using static BuildingManager;

public class BuildingGhost : MonoBehaviour
{
    public static event EventHandler<BuildingTypeSO> OnShowBuildingGhost;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Color _denyColor;
    [SerializeField] private Color _allowColor;
    private SpriteRenderer _spriteRenderer;
    private LineRenderer _lineRenderer;
    private BuildingTypeSO _activateBuildingType;
    private PolygonCollider2D _activeBuildingTypeCollider;
    private PolygonCollider2D _currentBuildingTypeCollider;


    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _lineRenderer = GetComponent<LineRenderer>();
        _currentBuildingTypeCollider = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        Hide();
    }

    private void OnDestroy()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged -= BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _spriteRenderer.color = _denyColor;
        BuildingManager.Instance.SetCanPlaceBuilding(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _spriteRenderer.color = _allowColor;
        BuildingManager.Instance.SetCanPlaceBuilding(true);
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, OnActiveBuildingTypeChangedEventArgs eventArgs)
    {
        if (eventArgs.Sprite == null)
        {
            Hide();
            return;
        }
        _activeBuildingTypeCollider = eventArgs.BuildingType.Prefab.GetComponent<PolygonCollider2D>();
        _currentBuildingTypeCollider.points = _activeBuildingTypeCollider.points;
        _activateBuildingType = eventArgs.BuildingType;
        Show(eventArgs.Sprite, eventArgs.ResourceDetectionRadius);
    }

    private void Update()
    {
        if (!this.enabled) { return; }

        transform.position = Utils.GetMouseWorldPosition();
    }

    private void Show(Sprite ghostSprite, float radius)
    {
        this.gameObject.SetActive(true);
        _spriteRenderer.sprite = ghostSprite;
        OnShowBuildingGhost?.Invoke(this, _activateBuildingType);
        DrawCircle(radius);
    }

    private void Hide()
    {
        OnShowBuildingGhost?.Invoke(this, null);
        this.gameObject.SetActive(false);
    }

    private void DrawCircle(float radius)
    {
        int segments = 100;
        _lineRenderer.positionCount = segments + 1;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;

        float angle = 0f;
        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            _lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += (360f / segments);
        }
    }
}
