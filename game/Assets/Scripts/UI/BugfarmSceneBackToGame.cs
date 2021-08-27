using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class BugfarmSceneBackToGame : MonoBehaviour
    {
        public void BackToGame() => SceneManager.LoadScene("Game");
    }
}
