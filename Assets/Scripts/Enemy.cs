﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour {
#pragma warning disable 0649
  [SerializeField] protected float speed;
#pragma warning restore 0649

  protected virtual void Start() {
    GetComponent<TreeDamageable>().SetOnDeathBehavior(OnDeath);
    if (transform.position.x > 0) speed = -speed;
  }

  protected virtual void Update() {
    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

    //Stop moving once we reach the tree trunk (at position x=0)
    if (speed > 0 && transform.position.x >= 0 || speed < 0 && transform.position.x <= 0) {
      transform.position = new Vector3(0, transform.position.y, transform.position.z);
      speed = 0;
      LevelGenerator.RemoveLife();
      OnDeath();
    }
  }

  protected virtual void OnDeath() {
    LevelGenerator.NotifyEnemyDied();
    Destroy(gameObject);
  }
}