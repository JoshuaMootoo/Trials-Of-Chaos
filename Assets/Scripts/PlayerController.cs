using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    public float movementSpeed = 10;

    private Rigidbody rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.useGravity = true;

        float moveLR, moveFB;
        moveFB = Input.GetAxis("Vertical") * movementSpeed;
        moveLR = Input.GetAxis("Horizontal") * movementSpeed;

        //  Debug.Log("moveFB" + moveFB + "moveLR" + moveLR);

        rb.velocity = new Vector3(moveLR, rb.velocity.y, moveFB);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            Transform newCenterTile = collision.transform;
            LevelManager.Instance.centerTile = newCenterTile;
            LevelManager.Instance.DetroyTiles();
        }
    }
}
