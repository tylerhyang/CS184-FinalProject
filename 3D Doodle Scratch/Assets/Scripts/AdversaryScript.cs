using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdversaryScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f; // The speed at which the enemy moves.
    public float chaseDistance = 10f; // The maximum distance at which the enemy can chase the player.
    public Transform player; // A reference to the player object.

    public float bounceForce = 30f;
    private bool playerSeen = false;

    private Rigidbody rb; // The enemy's Rigidbody component.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         Vector3 direction = (player.position - transform.position).normalized;

        // Calculate the distance from the enemy to the player.
        float distance = Vector3.Distance(player.position, transform.position);

        // If the player is within the chase distance, move towards them.
        if (distance <= chaseDistance || playerSeen)
        {
            playerSeen = true;
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the normal of the collision to determine if the player collided with the enemy from above
            Vector3 contactNormal = collision.contacts[0].normal.normalized;
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            Debug.Log(contactNormal.ToString());
            if (contactNormal.y < -0.75) //adding some room for error
            {
                // If the player collided with the enemy from above, destroy the enemy
                Destroy(gameObject);
            }
        }
    }
}
