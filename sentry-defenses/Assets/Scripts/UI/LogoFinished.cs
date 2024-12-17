using UnityEngine;

public class LogoFinished : MonoBehaviour
{
    public StartMenu StartMenu;

    // Gets called by an animation trigger
    public void OnLogoFinished() => StartMenu.LogoFinished();
}
