using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoSingleton<PlayerInput>
{
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public bool GetMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        return false;
    }
}
