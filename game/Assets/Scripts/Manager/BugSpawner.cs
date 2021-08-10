using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BugSpawner : MonoSingleton<BugSpawner>
{
    public List<BugData> Bugs;

    private Camera _camera;
    private float _spawnDistance;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _spawnDistance = Screen.width / 2.0f;
    }

    public GameObject Spawn()
    {
        var randomDirection = Random.insideUnitCircle;
        var randomScreenCirclePosition = _camera.WorldToScreenPoint(Vector3.zero) + new Vector3(randomDirection.x, randomDirection.y, 0).normalized * _spawnDistance;

        var randomPosition = _camera.ScreenToWorldPoint(randomScreenCirclePosition);

        var bug = Instantiate(Bugs[0].Prefab, randomPosition, Quaternion.identity);
        bug.transform.SetParent(transform);
        
        return bug;
    }
}
