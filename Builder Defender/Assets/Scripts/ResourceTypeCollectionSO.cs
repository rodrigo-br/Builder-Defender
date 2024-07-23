using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource Type Collection")]
public class ResourceTypeCollectionSO : ScriptableObject
{
    public List<ResourceTypeSO> List;
}
