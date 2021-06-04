using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coneBehav : MonoBehaviour
{
    private enemyController pai;

    void Start()
    {
        pai = gameObject.GetComponentInParent<enemyController>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") {
            pai.playerInSightRange = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player") {
            pai.playerInSightRange = false;
        }
    }
}
