using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text _scoreTextField;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private Text _gameOverScoreText;
    [SerializeField] private GameObject _gameOverFlicker;
    [SerializeField] private Image _livesSpriteImage;
    [SerializeField] private Sprite[] _livesSprite;
    // [SerializeField] private bool _isGameOver = true;


    void Start()
    {
        _scoreTextField.text = "Score: " + 0;
        _gameOverUI.SetActive(false);
    }

    public void updateScore(int playerScore)
    {
        _scoreTextField.text = "Score: " + playerScore;
    }

    public void updateLiveSprite(int livesRemaining)
    {
        _livesSpriteImage.sprite = _livesSprite[livesRemaining];
    }

    public void GameOverScreen(int lastScorePlayer)
    {
        _gameOverUI.SetActive(true);
        _gameOverScoreText.text = "Your Score: " + lastScorePlayer.ToString("0");
        StartCoroutine(flickerGameOverCooldown());

    }
    private IEnumerator flickerGameOverCooldown()
    {
        while (true)
        {
            Debug.Log("while is game over true is working");
            _gameOverFlicker.SetActive(true);
            yield return new WaitForSeconds(1f);
            _gameOverFlicker.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
    }

}
