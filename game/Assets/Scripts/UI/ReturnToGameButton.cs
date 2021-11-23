using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToGameButton : MonoBehaviour
{
    public void ReturnToGame() => SceneManager.LoadScene("Game");
}
