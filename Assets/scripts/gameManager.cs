using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    private bool hasGameEnded = false;
    private float delayInvoke = 0f;

    public void gameOver()
    {
        if (hasGameEnded == false)
        {
            hasGameEnded = true;
            Invoke("reStartGame", delayInvoke);
        }
    }

    private void reStartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
