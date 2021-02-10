using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    private Player _player;
    [SerializeField] private Text _scoreTextField;
    private int _scoreHolder;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
    }

    private void updateScore()
    {
        _scoreHolder = _player.currentScore();
        _scoreTextField.text = _scoreHolder.ToString("0");
    }
}
