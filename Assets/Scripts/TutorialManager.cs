﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{   
    public GameObject enemy;
    private bool learning = true;
    public float learnTime = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= learnTime && enemy != null) enemy.SetActive(true);
    }
}