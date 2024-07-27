using UnityEngine;

[CreateAssetMenu(fileName = "Building Type SO")]
public class BuildingTypeSO : ScriptableObject
{
    public string Name;
    public Transform Prefab;
    public ResourceGeneratorData ResourceGeneratorData;
}
