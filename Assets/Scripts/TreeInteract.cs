using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteract : MonoBehaviour
{
    // Start is called before the first frame update
    List<string> enemies;
    void Start()
    {
        //add types of enemy tags here
        enemies.Add("lumberjack");
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: kill enemy if touches tree ONLY IF tree is slamming AND enemy is not being animated
        //when killing an enemy, increasee score
        //when enemy enters trunk trigger, trigger animation with animals (and delay movements) -- when death zone entered, destroy both gameobjects
        //deal damage to tree trunk if reached (trigger game over, STOP game)
    }

    void OnTriggerEnter(Collider other){
        if (enemies.Contains(other.tag)) {

        }
    }
}
