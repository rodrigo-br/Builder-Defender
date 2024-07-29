using System;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    public BuildingTypeSO BuildingType { get; private set; }
    private ResourceGeneratorData _resourceGeneratorData;
    private LineRenderer _lineRenderer;
    private float _timerMax;
    private float _timer;
    private int _nearbyResourceAmount;
    private int _nearbyBuildingsPenalty = -1;

    private void Awake()
    {
        BuildingType = GetComponent<BuildingTypeHolder>().BuildingType;
        _lineRenderer = GetComponent<LineRenderer>();
        _resourceGeneratorData = BuildingType.ResourceGeneratorData;
        _timerMax = _resourceGeneratorData.TimerMax;
        DrawPenaltyRadius();
        _lineRenderer.enabled = false;
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
            ResourceManager.Instance.AddResource(_resourceGeneratorData.ResourceType, _nearbyResourceAmount - (_nearbyBuildingsPenalty * _resourceGeneratorData.BuildingPenaltyAmount));
        }
    }

    private void OnEnable()
    {
        BuildingGhost.OnShowBuildingGhost += CheckBuildingTypePenalty;
    }

    private void CheckBuildingTypePenalty(object sender, BuildingTypeSO e)
    {
        _lineRenderer.enabled = e == BuildingType;
    }

    public void DrawPenaltyRadius()
    {
        _lineRenderer.gameObject.SetActive(true);
        int segments = 100;
        _lineRenderer.positionCount = segments + 1;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;

        float angle = 0f;
        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * _resourceGeneratorData.BuildingPenaltyRadius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * _resourceGeneratorData.BuildingPenaltyRadius;
            _lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += (360f / segments);
        }
    }

    private void GetResourcesNearby()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _resourceGeneratorData.ResourceDetectionRadius);

        foreach (Collider2D collider in colliders)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if (resourceNode != null && resourceNode.ResourceTypeSO == _resourceGeneratorData.ResourceType)
            {
                _nearbyResourceAmount++;
            }
        }
    }

    private void GetBuildingsNearby()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _resourceGeneratorData.BuildingPenaltyRadius);
        foreach (Collider2D collider in colliders)
        {
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null && buildingTypeHolder.BuildingType == BuildingType)
            {
                _nearbyBuildingsPenalty++;
            }
        }
        Debug.Log($"Buildings Nearby: {_nearbyBuildingsPenalty}");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, _resourceGeneratorData.ResourceDetectionRadius);
    }
}
