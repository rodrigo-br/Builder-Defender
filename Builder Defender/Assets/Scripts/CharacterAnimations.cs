using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _mainSpriteRenderer;
    [SerializeField] private List<Sprite> _eSprites;
    [SerializeField] private List<Sprite> _seSprites;
    [SerializeField] private List<Sprite> _sSprites;
    [SerializeField] private List<Sprite> _swSprites;
    [SerializeField] private List<Sprite> _wSprites;
    [SerializeField] private List<Sprite> _nwSprites;
    [SerializeField] private List<Sprite> _nSprites;
    [SerializeField] private List<Sprite> _neSprites;
    [SerializeField] private float _directionalAnimationSpeed = 0.2f;
    private CharacterMovement _characterMovement;
    private float _timer;
    private int _directionalAnimationIndex;
    private int _directionalSpritesCount;

    private void Awake()
    {
        _characterMovement = GetComponentInParent<CharacterMovement>();
        _directionalSpritesCount = _nSprites.Count;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_characterMovement.Direction < 0) return;
        if (_timer >= _directionalAnimationSpeed)
        {
            _timer = 0f;
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        _directionalAnimationIndex = (_directionalAnimationIndex + 1) % _directionalSpritesCount;
        _mainSpriteRenderer.sprite = GetCurrentSprites()[_directionalAnimationIndex];
    }

    private List<Sprite> GetCurrentSprites()
    {
        return _characterMovement.Direction switch
        {
            >= 337.5f or < 22.5f => _eSprites,
            >= 292.5f => _seSprites,
            >= 247.5f => _sSprites,
            >= 202.5f => _swSprites,
            >= 157.5f => _wSprites,
            >= 112.5f => _nwSprites,
            >= 67.5f => _nSprites,
            >= 22.5f => _neSprites,
            _ => _nSprites,
        };
    }
}
