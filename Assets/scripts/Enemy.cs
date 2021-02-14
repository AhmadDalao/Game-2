using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _moveSpeed = 4f;
    private Player _player;
    [SerializeField] private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("player is not found");
        }
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("animator is not found");
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
            didHitPlayer();
        }
        if (other.tag == "Laser")
        {
            didHitLaser(other);
        }
    }

    private void didHitPlayer()
    {
        if (_player != null)
        {
            if (_animator != null)
            {
                _animator.SetTrigger("onEnemyDeath");
            }
            _moveSpeed = 1f;
            Destroy(this.gameObject, 0.25f);
            _player.playerTakeDamage();
        }
    }

    private void didHitLaser(Collider2D other)
    {
        int randomScore = Random.Range(5, 11);
        if (_animator != null)
        {
            _animator.SetTrigger("onEnemyDeath");
        }
        _moveSpeed = 1f;
        Destroy(this.gameObject, 0.25f);
        if (_player != null)
        {
            _player.updatePlayerScore(randomScore);
        }
        Destroy(other.gameObject);
    }

}
