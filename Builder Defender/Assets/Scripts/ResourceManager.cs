using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public event EventHandler OnResourceAmountChange;
    public static ResourceManager Instance { get; private set; }
    private Dictionary<ResourceTypeSO, float> _resourceAmountDictionary;

    private void Awake()
    {
        Instance = this;
        _resourceAmountDictionary = new Dictionary<ResourceTypeSO, float>();
        ResourceTypeCollectionSO resourceTypeCollection = Resources.Load<ResourceTypeCollectionSO>(typeof(ResourceTypeCollectionSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeCollection.List)
        {
            _resourceAmountDictionary[resourceType] = 0;
        }
    }

    public void AddResource(ResourceTypeSO resourceType, float amount)
    {
        _resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChange?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return (int)_resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmounts)
    {
        foreach (ResourceAmount resourceAmount in resourceAmounts)
        {
            if (GetResourceAmount(resourceAmount.ResourceType) < resourceAmount.Amount)
            {
                return false;
            }
        }
        return true;
    }

    public void SpendResources(ResourceAmount[] resourceAmounts)
    {
        foreach (ResourceAmount resourceAmount in resourceAmounts)
        {
            _resourceAmountDictionary[resourceAmount.ResourceType] -= resourceAmount.Amount;
        }
    }
}
