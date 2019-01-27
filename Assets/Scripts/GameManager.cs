using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameController gameController;
    public GameObject enemy;

    public static GameManager instance = null; //Allows GM to be accessed by other scripts.

    private static int lives = 6;
    private Text levelText;
    private Text instructionalText; //for tutorial
    private int level = -1; //start menu as -1 tutorial as 0, etc.

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
        if (level == 0){ //tutorial
            bool learning = true;
            if (learning){
                if (Mathf.Abs(gameController.Angle) < 45.0f){
                    learning = false;
                }
            }
            else enemy.SetActive(true); 
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
        level++;
    }
    void InitLevel()
    {
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Day " + level;
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