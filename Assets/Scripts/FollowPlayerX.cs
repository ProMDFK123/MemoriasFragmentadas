using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public Transform player;
    public Transform yReferencePoint;
    public float smoothSpeed = 5f;

    private float initialZ;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        initialZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player != null && yReferencePoint != null)
        {
            Vector3 desiredPos = new Vector3(player.position.x, yReferencePoint.position.y, initialZ);
            transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        }
    }
}
