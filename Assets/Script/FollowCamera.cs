using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Public variables
    public Transform target;
    public float followSpeed = 3.0f;
    public Vector2 minPos;
    public Vector2 maxPos;
    public LayerMask wallLayer;
    public float wallDistance = 1.0f;

    // Private variables
    Vector3 targetPos;
    float z;

    void Start()
    {
        // Store the starting z position
        z = transform.position.z;
    }

    void FixedUpdate()
    {
        // Set the target position to the player's position
        targetPos = target.position;
        targetPos.z = z;

        // Follow the target smoothly
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

        // Limit the camera position to the bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minPos.x, target.position.x);
        pos.y = Mathf.Clamp(pos.y, target.position.y, maxPos.y);
        transform.position = pos;

        // Check if the camera is colliding with any walls
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, wallDistance, wallLayer);
        if (hit.collider != null)
        {
            // If a wall is detected, move the camera closer to the target to avoid it
            transform.position = Vector3.Lerp(transform.position, target.position, hit.distance / wallDistance);
        }
    }
}

