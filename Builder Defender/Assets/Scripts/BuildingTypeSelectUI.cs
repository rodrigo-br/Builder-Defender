using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Transform _buttonTemplate;
    [SerializeField] private float offsetAmount = 160f;

    private void Awake()
    {
        _buttonTemplate.gameObject.SetActive(false);
        BuildingTypeCollectionSO buildingTypeCollection = Resources.Load<BuildingTypeCollectionSO>(typeof(BuildingTypeCollectionSO).Name);

        int currentOffsetAmountIndex = 0;
        foreach (BuildingTypeSO buildingType in buildingTypeCollection.List)
        {
            Transform buttonTransform = Instantiate(_buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            buttonTransform.Find("BuildingImage").GetComponent<Image>().sprite = buildingType.Sprite;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * currentOffsetAmountIndex, 0);

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });
            currentOffsetAmountIndex++;
        }
    }
}
