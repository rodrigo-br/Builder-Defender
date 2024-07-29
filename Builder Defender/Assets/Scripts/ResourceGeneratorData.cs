using System;
using UnityEngine;

[Serializable]
public class ResourceGeneratorData
{
    public float TimerMax;
    public ResourceTypeSO ResourceType;
    public float ResourceDetectionRadius;
    public float BuildingPenaltyRadius;
    public float BuildingPenaltyAmount;
}
