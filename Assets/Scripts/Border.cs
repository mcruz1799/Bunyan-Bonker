using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Border : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (WigglyTree.enemies.Contains(other.tag))
        {
            //An enemy has breached the safe zone.
            if (SceneManager.GetActiveScene().name != "Level0")
            { 
                GameManager.instance.SafetyBreached();
            }
            Destroy(other.gameObject);
        }
        if (other.tag == "Boss"){
                other.GetComponent<BossMechanics>().SwitchSides();
        }
    }
}
