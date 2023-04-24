using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlaneTrigger : MonoBehaviour
{
    public bool playerHit = false;

    void OnTriggerEnter(Collider Other) {
        if (Other.CompareTag("Player")) {
            playerHit = true;
        }
    }
}
