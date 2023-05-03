using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdversaryScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f; // The speed at which the enemy moves.
    public float chaseDistance = 10f; // The maximum distance at which the enemy can chase the player.
    public Transform player;
    public Transform followTarget;
    public bool playerSeen = false;
    public bool chasingPlayer = false;
    public float alertRadius = 10f;

    private Rigidbody rb; // The enemy's Rigidbody component.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1.0f, 0);
        player = GameObject.FindGameObjectWithTag("Player").transform; // A reference to the player object.
        followTarget = GameObject.FindGameObjectWithTag("Player").transform;
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
         Vector3 direction = (player.position - transform.position).normalized;

        // Calculate the distance from the enemy to the player.
        float distance = Vector3.Distance(player.position, transform.position);

        // alert distance logic to make enemies within range of the player to alert other chase enemies
        if (distance <= chaseDistance || playerSeen)
        {
            playerSeen = true;
            Collider[] colliders = Physics.OverlapSphere(transform.position, alertRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject != gameObject && collider.gameObject.tag == "chaseEnemy")
                {
            
            // Set follow target to player
                    collider.gameObject.GetComponent<AdversaryScript>().chasingPlayer = true;
                }
            }
        }

        if (chasingPlayer || playerSeen)
        {
        // Move towards follow target
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
            transform.LookAt(player);
        }
    }
    void OnDrawGizmosSelected()
{
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, alertRadius);
    Gizmos.color = Color.blue;
    Gizmos.DrawWireSphere(transform.position, chaseDistance);
}
}
