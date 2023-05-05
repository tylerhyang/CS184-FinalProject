using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootScript : MonoBehaviour
{
    public GameObject pelletPrefab;
    public Transform pelletSpawn;
    public float fireRate = 0.1f;

    public float pelletSpeed = 10f;
    private float nextFireTime = 0f;

    private float maxDistance = 100f;
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            FirePellet();
        }
    }

    void FirePellet()
    {
        GameObject pellet = Instantiate(pelletPrefab, pelletSpawn.position, pelletSpawn.rotation);

        // Rigidbody rb = pellet.GetComponent<Rigidbody>();
        // rb.velocity = transform.forward * pelletSpeed;

        // RaycastHit hit;
        // if (Physics.Raycast(pelletSpawn, pelletSpawn.forward, out hit, maxDistance, enemyLayer))
        // {
        //     Destroy(hit.collider.gameObject);
        // }
    }
}
