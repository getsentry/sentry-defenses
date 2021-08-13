using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    
    public void SwitchToZero()
    {
        SceneManager.LoadScene(0);
    }
    
    
    public void SwitchToOne()
    {
        SceneManager.LoadScene(1);
    }
    
    public void SwitchToTwo()
    {
        SceneManager.LoadScene(2);
    }
}
