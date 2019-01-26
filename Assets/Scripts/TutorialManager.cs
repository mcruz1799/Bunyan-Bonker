using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{   
    public GameObject enemy;
    public GameController gameController;
    private bool learning = true;
    public float learnTime = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {   
        if (learning){
            if (gameController.Angle > 45.0f || gameController.Angle < -45.0f){
            learning = false;
            }
        }
        else enemy.SetActive(true);
    }
}
