using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteract : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<string> enemies;
    bool slamming;
    int score;
    public int lives = 5;
    public int interval = 10; //
    void Start()
    {
        enemies = new List<string>();
        //add types of enemy tags here
        enemies.Add("lumberjack");

        slamming = false;
    }

    // Update is called once per frame
    void Update()
    {
        //gamemanager: handle animal instantiation, animal deletion, lumberjack animation trigger

    }


    //when enemy touches tree branches
    void OnColliderEnter(Collider other){
        if (enemies.Contains(other.tag)) {
            if (slamming /* && enemy is not being animated */) {
                Destroy(other.gameObject);

                //increase score
                score += interval;
            }
        }
    }
}
