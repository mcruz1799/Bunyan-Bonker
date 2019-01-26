using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;
    public Transform leftSpawnTransform;
    public Transform rightSpawnTransform;
    public float spawnRate = 5.0f;

    


    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("Spawn", 0.0f, spawnRate);
    }

    void Spawn(){
        Object.Instantiate(enemy, leftSpawnTransform);
        Object.Instantiate(enemy, rightSpawnTransform);
    }
}
