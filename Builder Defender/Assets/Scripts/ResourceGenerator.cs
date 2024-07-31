using System;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    public BuildingTypeSO BuildingType { get; private set; }
    public ResourceGeneratorData ResourceGeneratorData { get; private set; }
    private LineRenderer _lineRenderer;
    private float _timerMax;
    private float _timer;
    private int _nearbyResourceAmount;
    private int _nearbyBuildingsPenalty = -1;

    private void Awake()
    {
        BuildingType = GetComponent<BuildingTypeHolder>().BuildingType;
        _lineRenderer = GetComponent<LineRenderer>();
        ResourceGeneratorData = BuildingType.ResourceGeneratorData;
        _timerMax = ResourceGeneratorData.TimerMax;
        if (_lineRenderer != null)
        {
            DrawPenaltyRadius();
            _lineRenderer.enabled = false;
        }
        if (gameObject.CompareTag("Base"))
        {
            _nearbyResourceAmount = 1;
        }
    }

    private void Start()
    {
        GetResourcesNearby();
        GetBuildingsNearby();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer += _timerMax;
            ResourceManager.Instance.AddResource(ResourceGeneratorData.ResourceType, GetAmountGenerated());
        }
    }

    public float GetAmountGenerated()
    {
        return _nearbyResourceAmount * (1 - (_nearbyBuildingsPenalty * ResourceGeneratorData.BuildingPenaltyAmount));
    }

    private void OnEnable()
    {
        if (_lineRenderer != null)
        {
            BuildingGhost.OnShowBuildingGhost += CheckBuildingTypePenalty;
        }
    }

    private void OnDisable()
    {
        if (_lineRenderer != null)
        {
            BuildingGhost.OnShowBuildingGhost -= CheckBuildingTypePenalty;
        }
    }

    private void CheckBuildingTypePenalty(object sender, BuildingTypeSO e)
    {
        _lineRenderer.enabled = e == BuildingType;
    }

    private void DrawPenaltyRadius()
    {
        int segments = 100;
        _lineRenderer.positionCount = segments + 1;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;

        float angle = 0f;
        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * ResourceGeneratorData.BuildingPenaltyRadius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * ResourceGeneratorData.BuildingPenaltyRadius;
            _lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += (360f / segments);
        }
    }

    private void GetResourcesNearby()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ResourceGeneratorData.ResourceDetectionRadius);

        foreach (Collider2D collider in colliders)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if (resourceNode != null && resourceNode.ResourceTypeSO == ResourceGeneratorData.ResourceType)
            {
                _nearbyResourceAmount++;
            }
        }
    }

    private void GetBuildingsNearby()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ResourceGeneratorData.BuildingPenaltyRadius);
        foreach (Collider2D collider in colliders)
        {
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null && buildingTypeHolder.BuildingType == BuildingType)
            {
                _nearbyBuildingsPenalty++;
            }
        }
    }

    public float GetTimerNormalized()
    {
        return 1 - _timer / _timerMax;
    }
}
