using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_manager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverScore;
    [SerializeField] private GameObject _gameOverText;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _liveImage;
    [SerializeField] private GameObject _gameOverScreen;

    void Start()
    {
        _scoreText.text = "Score: " + 0;
    }

    public void updateScoreText(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString("0");
    }

    public void updateLivesOnDamage(int liveRemaining)
    {
        _liveImage.sprite = _livesSprites[liveRemaining];
    }

    public void gameOverScreen()
    {
        _gameOverScreen.SetActive(true);
        _gameOverScore.text = _scoreText.text;
        StartCoroutine(gameOverFlicker());
    }

    private IEnumerator gameOverFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(1f);
            _gameOverText.SetActive(true);
        }
    }
}
