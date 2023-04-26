using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 5f;

    private Vector3 originalPosition;
    private Vector3 movementDirection = Vector3.right;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
        if (Mathf.Abs(transform.position.x - originalPosition.x) >= moveDistance)
        {
            movementDirection = -movementDirection;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        movementDirection = -movementDirection;
    }
}
