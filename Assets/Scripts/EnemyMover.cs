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
            speed = 0.2f * level;
            //assuming tree is at 0, check which side we are on
            if(this.transform.position.x > 0) speed = -speed;
        } 
        else{
            speed = 0.05f * level;
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
<<<<<<< HEAD

    //triggered by animal script when they encounter an enemy
    public void AnimalDelay(float f){
        AnimalFight(f);
    }

    IEnumerator AnimalFight(float f){
        moving = false;
        yield return new WaitForSeconds(f);
        moving = true;
    }

    //Lumberjack has hit the border, run in opposite direction.
    public void RunAway()
    {
        speed = -speed;
        //TODO: Flip Animator/Sprite.
    }
=======
>>>>>>> 0fc7e1ae1e7e013620f646eba3d1f3a684488541
}
