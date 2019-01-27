using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMechanics : Enemy {
  Vector3 startPos;
  public Transform bullet;
  public Transform poofFX;
  public int bulletMax = 1;
  int hitcount = 0;
  bool moving = true;
  bool isShooting = false;

  public int hitMax = 2;
  protected override void Start() {
    base.Start();

    startPos = transform.position;
    GetComponent<TreeDamageable>().AddOnDamagedListener(GetHit);
  }

  protected override void Update() {
    if (LevelGenerator.State == LevelGenerator.GameState.InProgress) {
      if (moving) {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
      }

      if (Mathf.Abs(transform.position.x) <= 5) {
        LevelGenerator.RemoveLife();
        SwitchSides();
      }
    } else if (LevelGenerator.State == LevelGenerator.GameState.GameOver) {
      OnDeath();
    }
  }

  public void SwitchSides() {
    //move to opposite side [animation?]
    Instantiate(poofFX, transform.position, transform.rotation);
    StartCoroutine(moveAway());
  }
  IEnumerator moveAway(){
    this.GetComponentInChildren<SpriteRenderer>().enabled = false;
    transform.position = new Vector3(startPos.x * -1, startPos.y, startPos.z);
    yield return new WaitForSeconds(1);
    Instantiate(poofFX, transform.position, transform.rotation);
    yield return new WaitForSeconds(1);
    this.GetComponentInChildren<SpriteRenderer>().enabled = true;
    speed = -speed;
    startPos = new Vector3(startPos.x * -1, startPos.y, startPos.z);
  }

  private void GetHit() {
    SwitchSides();
    if (speed < 0) speed -= 0.01f;
    else speed += 0.005f;
    hitcount++;
    if (hitcount > hitMax && !isShooting) {
      StartCoroutine("ShootBullets");
      isShooting = true;
      moving = false;
    }
  }

  private IEnumerator ShootBullets() {
    int count = 0;
    while (LevelGenerator.State == LevelGenerator.GameState.InProgress) {
      if (count < bulletMax) {
        Vector3 pos;
        if (transform.position.x > 0) {
          pos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        } else {
          pos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        Transform shot = Instantiate(bullet, pos, transform.rotation);
        shot.transform.parent = this.transform;
        count++;
        yield return new WaitForSeconds(3);
      } else {
        break;
      }
    }
    isShooting = false;
    moving = true;
    yield return new WaitForSeconds(0);
  }

  //when tree hits boss, switch sides + up speed
  //when boss hits tree, switch sides
}
