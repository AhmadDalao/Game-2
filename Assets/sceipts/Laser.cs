using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Laser Move speed")]
    [SerializeField] private float _moveSpeed = 8f;

    // Update is called once per frame
    void Update()
    {
        // move the laser up
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
        float maxY = 7f;
        // clean up the laser shot
        if (transform.position.y > maxY)
        {
            Destroy(this.gameObject);
        }
    }
}
