using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _moveSpeed = 2.5f;
    private Player _player;
    [SerializeField] private Animator _animator;
    private AudioSource _explosionSoundOnEnemy;
    [SerializeField] private GameObject _enemyLaserPrefab;
    private GameObject _laserContainer;
    private GameObject _clonedLaser;
    private float _lastShot = -1f;
    private float _shotingRate = 3.0f;
    private float _offset = 1f;

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

        _explosionSoundOnEnemy = GetComponent<AudioSource>();
        if (_explosionSoundOnEnemy == null)
        {
            Debug.LogError("_explosionSoundOnEnemy is null on enemy script");
        }

        _laserContainer = GameObject.FindWithTag("LaserContainer");
        if (_laserContainer == null)
        {
            Debug.LogError("_laserContainer is null on enemy script");
        }
    }

    void Update()
    {
        enemyMovement();
        enemyFire();
    }

    private void enemyMovement()
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
            _explosionSoundOnEnemy.Play();
            didHitPlayer();
        }
        if (other.tag == "Laser")
        {
            _explosionSoundOnEnemy.Play();
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
            _player.playerTakeDamage();
            Destroy(this.GetComponent<Collider2D>());
            Destroy(this.gameObject, 1f);
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
        if (_player != null)
        {
            _player.updatePlayerScore(randomScore);
        }
        Destroy(this.GetComponent<Collider2D>());
        Destroy(this.gameObject, 1f);
        Destroy(other.gameObject);
    }

    private void enemyFire()
    {
        if (Time.time > _lastShot)
        {
            _lastShot = Time.time + _shotingRate;
            Vector3 laserPosition = new Vector3(transform.position.x, transform.position.y - _offset, 0);
            _clonedLaser = Instantiate(_enemyLaserPrefab, laserPosition, Quaternion.identity);
            //   _clonedLaser.transform.SetParent(_laserContainer.transform);
            Laser[] lasers = _clonedLaser.GetComponentsInChildren<Laser>();
            foreach (var laser in lasers)
            {
                laser.isEnemyLaserFired();
            }
        }
    }

}
