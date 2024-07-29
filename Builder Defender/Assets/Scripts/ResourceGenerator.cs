using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData _resourceGeneratorData;
    private float _timerMax;
    private float _timer;
    private int _nearbyResourceAmount;

    private void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().BuildingType.ResourceGeneratorData;
        _timerMax = _resourceGeneratorData.TimerMax;
    }

    private void Start()
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

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer += _timerMax;
            ResourceManager.Instance.AddResource(_resourceGeneratorData.ResourceType, _nearbyResourceAmount);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, _resourceGeneratorData.ResourceDetectionRadius);
    }
}
