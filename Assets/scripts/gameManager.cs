using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    private bool _hasGameEnded = false;
    private float _delayInvoke = 0f;

    public void gameOver()
    {
        if (_hasGameEnded == false)
        {
            _hasGameEnded = true;
            Invoke("reStartGame", _delayInvoke);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void reStartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

}
