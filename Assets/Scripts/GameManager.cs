using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null; //Allows GM to be accessed by other scripts.

    private static int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Game State: Lumberjacks reached the tree.
     * 1. Remove an animal
     * 2. Update Lives
     * 3. Send Lumberjacks offscreen.
                                                  */
    public void SafetyBreached()
    {

    }
}
