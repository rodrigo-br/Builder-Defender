using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource Type")]
public class ResourceTypeSO : ScriptableObject
{
    public string Name;
    public Transform Prefab;
}
