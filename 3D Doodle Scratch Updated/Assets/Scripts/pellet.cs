using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pellet : MonoBehaviour
{ 
    public float speed = 20f;
    public float lifespan = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifespan);
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("platform") && !collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
