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
        InvokeRepeating("Spawn", 2.0f, spawnRate);

    }

    // Update is called once per frame
    void Update()
    {
    }

    void Spawn(){
        Quaternion rotation = new Quaternion(0,0,0,0);
        Object.Instantiate(enemy, leftSpawnTransform.position, rotation );
        Object.Instantiate(enemy, rightSpawnTransform.position, rotation);
    }
}
