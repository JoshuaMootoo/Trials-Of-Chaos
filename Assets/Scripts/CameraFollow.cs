using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Transform player; // The player object the camera will follow
    [SerializeField] private Vector3 offsetPos;
    public float smoothSpeed = 0.125f; // How quickly the camera will follow the target

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>().transform;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offsetPos;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }

    }
}
