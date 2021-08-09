using UnityEngine;

public class GameData : MonoSingleton<GameData>
{
    public GameObject SentryPrefab;


    public int MaxSentryCount = 3;
    public int PlacedSentryCount;
}
