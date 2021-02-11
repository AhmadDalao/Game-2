using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Move speed")]
    [SerializeField] private float _moveSpeed = 4f;
    private Player _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        float minY = -8f;
        if (transform.position.y < minY)
        {
            float minX = -8;
            float maxX = 8;
            float random = Random.Range(minX, maxX);
            transform.position = new Vector3(random, transform.position.y * -1f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            if (_player != null)
            {
                _player.damagePlayer();
            }
        }
        if (other.tag == "Laser")
        {
            if (this.gameObject.tag == "Enemy")
            {
                if (_player != null)
                {
                    Debug.Log("I have been called");
                    int randomPoint = Random.Range(5, 13);
                    _player.addScore(randomPoint);
                    Destroy(this.gameObject);
                }
            }
            Destroy(other.gameObject);
        }
    }

}
