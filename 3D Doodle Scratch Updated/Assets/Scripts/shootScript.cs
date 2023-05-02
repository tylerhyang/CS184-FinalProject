using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootScript : MonoBehaviour
{
    public GameObject pelletPrefab;
    public Transform pelletSpawn;
    public float fireRate = 0.1f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            FirePellet();
        }
    }

    void FirePellet()
    {
        Instantiate(pelletPrefab, pelletSpawn.position, pelletSpawn.rotation);
    }
}
