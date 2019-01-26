using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null; //Allows GM to be accessed by other scripts.

    private static int lives = 6;

    // private static bool tutorial = false; 


    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
        Scene scene = SceneManager.GetActiveScene();
        // if (scene.buildIndex == 1) tutorial = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        addLives();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Game State: Lumberjacks reached the tree, Players lose a 'life' (or animal.)
                                                  */
    public void SafetyBreached()
    {
        lives -= 1;
        Debug.Log("Lives Left:" + lives);
        removeAnimal();
        checkGameOver();
    }

    void checkGameOver()
    {
        if (lives <= 0)
        {
            //TODO: Replace with Game Over State.
            #if  UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Debug.Log("Game Over.");
                Application.Quit();
            #endif
        }
    }

    //Adds Animals representing the Lives.
    void addLives()
    {
        for (int i = 0; i < lives; i++)
        {
            //TODO: Replace with random selection from animals.
            Instantiate(Resources.Load("256px"), new Vector3(Random.Range(-4, 4), 2, 0),transform.rotation);
        }
    }

    void removeAnimal()
    {
        GameObject[] Animals;
        Animals = GameObject.FindGameObjectsWithTag("Animal");

        if (Animals.Length == 0)
        {
            Debug.Log("All the animals have already left. :(");
        } else
        {
            int index = Random.Range(0, Animals.Length);
            GameObject animal = Animals[index];
            Destroy(animal);
        }
    }
}
