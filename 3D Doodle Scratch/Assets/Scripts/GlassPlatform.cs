using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassPlatform : MonoBehaviour
{

    public CharacterController controller;
    public Transform Player;

    void Start()
    {
        
        Player = GameObject.FindWithTag("Player").transform;
        controller = GameObject.FindWithTag("Player").controller;  
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);
        if (controller.isGrounded && distance < 0.5)
        {
            Destroy(gameObject, 0.5f);
        }
        
    }
    
    // private void OnCollisionEnter(Collision collision)
    // {

    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         bool isOnTop = false;
    //         ContactPoint[] contacts = new ContactPoint[10];
    //         int numContacts = collision.GetContacts(contacts);
    //         for (int i = 0; i < numContacts; i++) {
    //             if (contacts[i].point.y > transform.position.y) {
    //                 isOnTop = true;
    //                 break;
    //             }
    //         }
    //         if (isOnTop) {
    //             Destroy(gameObject, 0.5f);
    //         }
    //     }
    // }
}
