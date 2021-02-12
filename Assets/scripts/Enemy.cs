using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _moveSpeed = 4f;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("player is not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        float min_y = -8f;
        if (transform.position.y < min_y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                Destroy(this.gameObject);
                _player.playerTakeDamage();
            }
        }
        if (other.tag == "Laser")
        {
            int randomScore = Random.Range(5, 11);
            Destroy(this.gameObject);
            if (_player != null)
            {
                _player.updatePlayerScore(randomScore);
            }
            Destroy(other.gameObject);
        }
    }

}
