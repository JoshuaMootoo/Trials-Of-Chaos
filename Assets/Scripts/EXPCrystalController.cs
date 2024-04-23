using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPCrystalController : MonoBehaviour
{
    public int xpAmount = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.GainExp(xpAmount);
            }
        }
    }
}
