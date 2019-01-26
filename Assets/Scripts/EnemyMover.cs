using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMover : MonoBehaviour
{
    int level;
    float speed;
    bool moving;
    void Start()
    {
        moving = true;
        string name = SceneManager.GetActiveScene().name;

        //DANGEROUS: assumes level name is a number w/at least 1 char
        level = int.Parse(name.Substring(name.Length - 1));

        //some enemies faster than others
        if (this.tag == "squirrel") {
            //increase speed per level
            speed = 0.05f * level;
            //assuming tree is at 0, check which side we are on
            if(this.transform.position.x > 0) speed = -speed;
        } 
        else{
            speed = 0.02f * level;
            if(this.transform.position.x > 0) speed = -speed;
        }
    }

   void Update()
    {
        //die if touches tree ONLY IF tree is slamming [tree script - OnTriggerEnter?]
        //does this enemy's death increase score? [again tree script]
        //interact with animals (and delay movements) [animal script - enemy rigidbodies] -- when trigger entered, destroy both gameobjects
        //deal damage to tree trunk if reached (trigger game over, STOP game) [tree script]

        //assuming we're in the x-y plane, move in x direction
        if (moving) {
            this.transform.Translate(new Vector3(speed, 0, 0));
        }

        //stop moving once we reach the tree trunk (at position x=0)
        if (speed > 0 && transform.position.x >=0 || 
                speed < 0 && transform.position.x <= 0)
        {
            transform.position = new Vector3(0, transform.position.y, 
                transform.position.z);
            moving = false;
        }
    }
}
