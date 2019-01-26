using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (TreeInteract.enemies.Contains(other.tag))
        {
            //An enemy has breached the safe zone.
            GameManager.instance.SafetyBreached();
            
            Destroy(other.gameObject);
        }
    }
}
