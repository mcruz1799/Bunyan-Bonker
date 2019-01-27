using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;
    public Transform leftSpawnTransform;
    public Transform rightSpawnTransform;
    public float spawnProbability = 2.0f;
    private int failCounter = 0;
    public int failMax = 5;
    public float spawnDelay = 4.0f;
    public float spawnInterval = 1.0f; //time interval for coroutine
    public float pauseDelay = 1.0f; //time after a spawn to reactivate spawning
    private bool pause = false;
    private float pauseTime = 0.0f;
    Quaternion rotation = new Quaternion(0,0,0,0);



    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine("Spawn");
    }

    public IEnumerator Spawn(){
        yield return new WaitForSeconds(spawnDelay);
        while(true){
            if (Time.time - pauseTime >= pauseDelay) pause = false;
            if (!pause){
                float currLeft = Random.Range(0.0f, 10.0f);
                if (currLeft <= spawnProbability)
                {
                    SpawnLeft();
                    pause = true;
                    pauseTime = Time.time;
                }
                else
                {
                    failCounter++;
                }
                float currRight = Random.Range(0.0f, 10.0f);
                if (currRight <= spawnProbability)
                {
                    SpawnRight();
                    pause = true;
                    pauseTime = Time.time;
                }
                else
                {
                    failCounter++;
                }
                failCheck();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void SpawnLeft(){
        Object.Instantiate(enemy, leftSpawnTransform.position, rotation );
    }
    void SpawnRight(){
        Object.Instantiate(enemy, rightSpawnTransform.position, rotation);

    }
    void failCheck()
    {
        if (failCounter >= failMax){
            float temp = Random.Range(0.0f, 10.0f);
            if (temp > 5) SpawnLeft();
            else SpawnRight();
            failCounter = 0;
        }
    }
}
