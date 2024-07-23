using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building Type Collection")]
public class BuildingTypeCollectionSO : ScriptableObject
{
    public List<BuildingTypeSO> List;
}
