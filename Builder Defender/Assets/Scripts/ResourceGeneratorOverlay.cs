using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator _resourceGenerator;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _barImage;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _iconImage.sprite = _resourceGenerator.ResourceGeneratorData.ResourceType.Sprite;
        _text.text = $"{_resourceGenerator.GetAmountGenerated()}";
    }

    private void Update()
    {
        _barImage.fillAmount = _resourceGenerator.GetTimerNormalized();
    }
}
