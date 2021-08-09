using UnityEngine;

[CreateAssetMenu(menuName = "Data/New Bug")]
public class BugData : ScriptableObject
{
    public string Name;
    public GameObject Prefab;

    public int HitPoints;
}
