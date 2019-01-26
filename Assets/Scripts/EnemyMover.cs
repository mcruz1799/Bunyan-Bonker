using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMover : MonoBehaviour
{
    int level;
    float speed;
    void Start()
    {
        string name = SceneManager.GetActiveScene().name;
        //DANGEROUS: assumes level name is a number w/at least 1 char
        level = int.Parse(name.Substring(name.Length - 1));
        //increase speed per level
        speed = 10 * level;
    }

   void Update()
    {
        //die if touches tree ONLY IF tree is slamming [tree script - OnTriggerEnter?]
        //does this enemy's death increase score? [again tree script]
        //do NOT hurt tree if touch branch 
        //do not interact with each other 

        //assuming we're in the x-y plane
        this.transform.Translate(new Vector3(speed, 0, 0));

        //some enemies faster than others
        if(this.tag == "squirrel"){

        }
        else{

        }

        //interact with animals (and delay movements) [colliders: other.tag]

        
        //deal damage to tree trunk if reached (trigger game over, STOP game) [tree script]
            //do not keep moving once reached tree trunk (never move offscreen)
    }
}
