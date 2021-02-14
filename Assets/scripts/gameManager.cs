using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    // this class is used with the quit and play again button
    // which is displayed on the gameOverPanel once a placed is died
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
