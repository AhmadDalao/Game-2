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

    void Start()
    {
        // _animator = GetComponent<Animator>();
        // if (_animator == null)
        // {
        //     Debug.LogError("asteroid class _animator does not exist");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * _tiltAroundZ * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Debug.Log("I got hit by a laser");
            _explosionPosition = new Vector3(transform.position.x, transform.position.y, 0);
            _explosionHolder = Instantiate(_explosion, _explosionPosition, Quaternion.identity);
            Destroy(other.gameObject); //  destroy the laser 
            Destroy(this.gameObject, 0.175f); // destroy the asteroid
            Destroy(_explosionHolder, 1.5f); // destroy the exploision
        }
    }
}
