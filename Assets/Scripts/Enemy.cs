using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour {
#pragma warning disable 0649
  [SerializeField] protected float speed;
  [SerializeField] protected Transform deathFX;
#pragma warning restore 0649

  protected virtual void Start() {
    GetComponent<TreeDamageable>().SetOnDeathBehavior(OnDeath);
    if (transform.position.x > 0) {
      speed = -speed;
      transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
  }

  protected virtual void Update() {
    if (LevelGenerator.State == LevelGenerator.GameState.InProgress) {
      transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

      //Stop moving once we reach the tree trunk (at position x=0)
      if (Mathf.Abs(transform.position.x) <= 5) {
        speed = 0;
        LevelGenerator.RemoveLife();
        OnDeath();
      }
    } else if (LevelGenerator.State == LevelGenerator.GameState.GameOver) {
      OnDeath();
    }
  }

  protected virtual void OnDeath() {
    Instantiate(deathFX, transform.position, transform.rotation);
    LevelGenerator.NotifyEnemyDied();
    Destroy(gameObject);
  }
}
