using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public WigglyTree wigglyTree;
    public GameObject enemy;

    public static GameManager instance = null; //Allows GM to be accessed by other scripts.

    private static int lives = 6;
    private Text levelText;
    private Text instructionalText; //for tutorial
    public string instruction1 = "Press and hold the spacebar to wiggle!";
    public string instruction2 = "The Bunyans are coming! Protect you and your furry friends!";
<<<<<<< HEAD
    private int level; //start menu as -1 tutorial as 0, etc.
=======
    private int level = -1; //start menu as -1 tutorial as 0, etc.
>>>>>>> parent of 08b8eef... GameManager transitions gah
    private bool learning = true; 

    private void Awake()
    {   
        level = SceneManager.GetActiveScene().buildIndex - 1;
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
<<<<<<< HEAD
        if (level == 0)
        {
            if (learning)
=======
        if (level == 0 && learning)
        { //tutorial
            instructionalText = GameObject.Find("InstructionalText").GetComponent<Text>();
            instructionalText.text = instruction1;
            if (Mathf.Abs(wigglyTree.Angle) < 45.0f) 
>>>>>>> parent of 08b8eef... GameManager transitions gah
            {
                learning = false;
                instructionalText.text = instruction2;
                enemy.SetActive(true);
            }
             
        }
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
    void OnLevelWasLoaded(int index)
    {   
        InitLevel(); 
<<<<<<< HEAD
        if (level == 2 || level == 3) addLives();
=======
        level++;
>>>>>>> parent of 08b8eef... GameManager transitions gah
    }
    void InitLevel()
    {
        Debug.Log("Init: " + level);
        if (level >= 0) 
        {
            levelText = GameObject.Find("LevelText").GetComponent<Text>();
            levelText.text = "Day " + level;
        }
        if (level >= 1)
        {
            // instructionalText.gameObject.SetActive(false);
        }
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
<<<<<<< HEAD
    void GameOver()
    {
        levelText.text = "The Bunyans chopped you down!";
    }
=======
>>>>>>> 85bf66f2855c332355103d2fc4c3f0c63f374017

    void GameOver()
    {
        levelText.text = "The Bunyans chopped you down!";
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
