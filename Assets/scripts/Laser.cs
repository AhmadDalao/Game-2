using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _moveSpeed = 12f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
        if (transform.position.y > 7)
        {
            if (transform.parent.gameObject.tag == "triple_shot_container" && transform.gameObject != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
