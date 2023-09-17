using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Vector3 followTarget;
    public float followDistance;
    public float moveSpeed;
    public float rotateSpeed;
    public float transportDistance;
    public float acceleration;
    public float moderateRatio;
    public float maxAngle;
    Vector3 velocity;
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
        UpdatePosition();
        UpdateRotation();
    }

    void UpdateRotation()
    {
        Vector3 delta = followTarget - transform.position;
        Vector3 targetDirection = delta.normalized;
        Vector3 currentDirection = transform.forward;
        Vector3 updateDirection = Vector3.Cross(Vector3.Cross(currentDirection, targetDirection).normalized, currentDirection);
        float ratio = (1 - Vector3.Dot(currentDirection, targetDirection)) / 2;
        if (ratio > maxAngle)
        {
            Vector3 nextDirection = currentDirection + updateDirection * rotateSpeed * Time.deltaTime * ratio;
            transform.LookAt(transform.position + nextDirection, Vector3.up);
        }
    }

    void UpdatePosition()
    {
        Vector3 delta = followTarget - transform.position;
        Vector3 targetDirection = delta.normalized;
        Vector3 moveTarget = followTarget - targetDirection * followDistance;
        Vector3 moveDirection = moveTarget - transform.position;
        if (moveDirection.magnitude < transportDistance)
        {
            transform.position = moveTarget;
            velocity = Vector3.zero;
        }
        else
        {
            Vector3 targetVelocity = moveDirection.normalized * moveSpeed * Mathf.Clamp01(moveDirection.magnitude * moderateRatio);
            if ((targetVelocity - velocity).magnitude < acceleration) velocity = targetVelocity;
            else velocity += (targetVelocity - velocity).normalized * acceleration;
            velocity.y = 0;
            transform.position += velocity * Time.deltaTime;
        }
    }
}
