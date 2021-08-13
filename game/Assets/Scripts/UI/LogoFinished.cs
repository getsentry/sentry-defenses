using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoFinished : MonoBehaviour
{
    public StartMenu StartMenu;

    // Gets called by an animation trigger
    public void OnLogoFinished()
    {
        Debug.Log("logo finished");
        StartMenu.LogoFinished();
    }
}
