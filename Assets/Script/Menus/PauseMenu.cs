using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("QuitGame", 3f);
    }


    private void QuitGame()
    {
        print("quitte");
        Application.Quit();
    }
}
