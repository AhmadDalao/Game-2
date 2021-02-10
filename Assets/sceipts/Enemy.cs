using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Move speed")]
    [SerializeField] private float _moveSpeed = 4f;
    private Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
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
        //    Player player = other.transform.GetComponent<Player>();
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            if (player != null)
            {
                player.damagePlayer();
            }
        }
        if (other.tag == "Laser")
        {
            if (this.gameObject.tag == "Enemy")
            {
                if (player != null)
                {
                    Debug.Log("I have been called");
                    Destroy(this.gameObject);
                    player.addScore();
                }
            }
            Destroy(other.gameObject);
        }
    }

}
