using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private Transform _resourceTemplate;
    [SerializeField] private float offsetAmount = -160f;
    private ResourceTypeCollectionSO _resourceTypeCollection;
    private Dictionary<ResourceTypeSO, Transform> _resourceTypeTransformDictionary;

    private void Awake()
    {
        _resourceTypeCollection = Resources.Load<ResourceTypeCollectionSO>(typeof(ResourceTypeCollectionSO).Name);
        _resourceTypeTransformDictionary = new();

        _resourceTemplate.gameObject.SetActive(false);
        int currentOffsetAmountIndex = 0;
        foreach (ResourceTypeSO resourceType in _resourceTypeCollection.List)
        {
            Transform resourceTransform = Instantiate(_resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * currentOffsetAmountIndex, 0);
            resourceTransform.GetComponentInChildren<Image>().sprite = resourceType.Sprite;

            _resourceTypeTransformDictionary[resourceType] = resourceTransform;
            currentOffsetAmountIndex++;
        }
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChange += ResourceManager_OnResourceAmountChange;
        UpdateResourceAmount();
    }

    private void OnDisable()
    {
        ResourceManager.Instance.OnResourceAmountChange -= ResourceManager_OnResourceAmountChange;
    }

    private void ResourceManager_OnResourceAmountChange(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in _resourceTypeCollection.List)
        {
            Transform resourceTransform = _resourceTypeTransformDictionary[resourceType];
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceTransform.GetComponentInChildren<TextMeshProUGUI>().SetText($"{resourceAmount}");
        }
    }
}
