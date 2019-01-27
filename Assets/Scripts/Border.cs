using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Border : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (TreeInteract.enemies.Contains(other.tag))
        {
            //An enemy has breached the safe zone.
            if (SceneManager.GetActiveScene().name != "Level0")
            { 
                GameManager.instance.SafetyBreached();
            }
            Destroy(other.gameObject);
        }
    }
}
