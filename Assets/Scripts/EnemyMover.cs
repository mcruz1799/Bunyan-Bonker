using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMover : MonoBehaviour
{
    int level;
    float speed;
    bool moving;
    public float movGen = 0.02f;
    public float movLum = 0.05f;
    void Start()
    {
        GetComponent<TreeDamageable>().SetOnDeathBehavior(() => Destroy(gameObject));
        moving = true;
        string name = SceneManager.GetActiveScene().name;

        //DANGEROUS: assumes level name is a number w/at least 1 char
        level = int.Parse(name.Substring(name.Length - 1));

        //some enemies faster than others
        if (this.tag == "lumberjack") {
            //increase speed per level
            speed = movLum * level;
            //assuming tree is at 0, check which side we are on
            if(this.transform.position.x > 0) speed = -speed;
        }
        else{
            speed = movGen; //* level;
            if(this.transform.position.x > 0) speed = -speed;
        }
    }

   void Update()
    {
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


    //triggered by animal script when they encounter an enemy
    public void AnimalDelay(float f){
        AnimalFight(f);
    }

    IEnumerator AnimalFight(float f){
        moving = false;
        yield return new WaitForSeconds(f);
        moving = true;
    }
}
