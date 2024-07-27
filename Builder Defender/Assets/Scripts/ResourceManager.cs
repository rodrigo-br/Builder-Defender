using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public event EventHandler OnResourceAmountChange;
    public static ResourceManager Instance { get; private set; }
    private Dictionary<ResourceTypeSO, int> _resourceAmountDictionary;
    private IInput _playerInput;
    private FrameInput _frameInput;

    private void Awake()
    {
        Instance = this;
        _resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeCollectionSO resourceTypeCollection = Resources.Load<ResourceTypeCollectionSO>(typeof(ResourceTypeCollectionSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeCollection.List)
        {
            _resourceAmountDictionary[resourceType] = 0;
        }
        TestLogResourceAmountDictionary();
    }

    private void Start()
    {
        _playerInput = PlayerInput.Instance;
    }

    private void Update()
    {
        _frameInput = _playerInput.GatherInput();
        if (_frameInput.MouseClick)
        {
            TestLogResourceAmountDictionary();
        }
    }

    private void TestLogResourceAmountDictionary()
    {
        foreach (ResourceTypeSO resourceType in _resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType + ": " + _resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        _resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChange?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return _resourceAmountDictionary[resourceType];
    }
}
