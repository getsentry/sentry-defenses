using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    Debug.LogError($"It truly broke. Could not find <color=red>'{typeof(T)}'</color>.");
                }
            }

            return _instance;
        }
        private set => _instance = value;
    }
}
