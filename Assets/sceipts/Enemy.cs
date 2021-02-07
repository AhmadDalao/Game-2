using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Move speed")]
    [SerializeField] private float _moveSpeed = 4f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        float minY = -8f;
        if (transform.position.y < minY)
        {
            float minX = -10;
            float maxX = 10;
            float random = Random.Range(minX, maxX);
            transform.position = new Vector3(random, transform.position.y * -1f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("you hit a this.gameobject.name" + this.gameObject.name); // enemy
            Destroy(this.gameObject);
            Debug.Log("you hit a other.gameobject.name" + other.gameObject.name); // player
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.damagePlayer();
            }
        }
        if (other.tag == "Laser")
        {
            Debug.Log("you hit a this.gameobject.name" + this.gameObject.name); // enemy
            Destroy(this.gameObject);
            Debug.Log("you hit a other.gameobject.name" + other.gameObject.name); // laser
            Destroy(other.gameObject);
        }
    }

}
