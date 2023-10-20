using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10;

    private Rigidbody rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddForce(Vector3.down, ForceMode.Acceleration);

        float moveLR, moveFB;
        moveFB = Input.GetAxis("Vertical") * movementSpeed;
        moveLR = Input.GetAxis("Horizontal") * movementSpeed;

        Debug.Log("moveFB" + moveFB + "moveLR" + moveLR);

        rb.velocity = new Vector3(moveLR, 0, moveFB);
    }
}
