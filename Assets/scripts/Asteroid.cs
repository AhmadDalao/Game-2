using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    private float _tiltAroundZ = 45f;
    // [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _explosion;
    private GameObject _explosionHolder;
    private Vector3 _explosionPosition;
    private spawnManager _spawnManager;

    void Start()
    {
        _spawnManager = GameObject.FindWithTag("spawn_manager").GetComponent<spawnManager>();
        if (_spawnManager == null)
        {
            Debug.Log("Asteroid:: spawn manager handler does not exist");
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.back * _tiltAroundZ * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _explosionPosition = new Vector3(transform.position.x, transform.position.y, 0);
            _explosionHolder = Instantiate(_explosion, _explosionPosition, Quaternion.identity);
            Destroy(other.gameObject); //  destroy the laser 
            Destroy(_explosionHolder, 1.5f); // destroy the exploision
            if (_spawnManager != null)
            {
                _spawnManager.isAsteroidDestroyed();
            }
            Destroy(this.gameObject, 0.175f); // destroy the asteroid
        }
    }
}
