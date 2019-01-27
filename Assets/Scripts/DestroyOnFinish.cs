using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnFinish : MonoBehaviour
{
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();   
        ps.GetComponent<Renderer>().sortingLayerName = "Foreground";    
    }

    void Update(){
        if(!ps.IsAlive()){
            Destroy(this.gameObject);
        }
    }
}
