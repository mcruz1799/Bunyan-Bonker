using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMechanics : Enemy
{
    public float movGen = 0.01f;
    Vector3 startPos;
    public Transform bullet;
    public int bulletMax = 1;
    int hitcount = 0;
    bool moving = true;
    bool isShooting = false;

    public int hitMax = 2;
    void Start()
    {
        startPos = transform.position;
        GetComponent<TreeDamageable>().SetOnDeathBehavior(() => OnDeath());
        speed = movGen;
        if(this.transform.position.x > 0) speed = -speed;
        this.GetComponent<TreeDamageable>().AddOnDamagedListener(GetHit);
    }

   void Update()
    {
        //assuming we're in the x-y plane, move in x direction
        if(moving) this.transform.Translate(new Vector3(speed, 0, 0));
        if (speed > 0 && transform.position.x >= 0 || speed < 0 && transform.position.x <= 0) {
            LevelGenerator.RemoveLife();
            SwitchSides();
        }
    }

    public void SwitchSides(){
        //move to opposite side [animation?]
        this.transform.position = new Vector3(startPos.x * -1, startPos.y,
            startPos.z);
        speed = -speed;
        startPos = new Vector3(startPos.x * -1, startPos.y, startPos.z);
    }

    void GetHit(){
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

    IEnumerator ShootBullets(){
        int count = 0;
        while (true){
            if (count < bulletMax) {
                Vector3 pos;
                if (transform.position.x > 0) pos = new Vector3
                    (transform.position.x - 1, transform.position.y, 
                        transform.position.z); 
                else pos = new Vector3(transform.position.x + 1, 
                    transform.position.y, transform.position.z);
                Transform shot = Instantiate(bullet, pos, transform.rotation);
                shot.transform.parent = this.transform;
                count++;
                yield return new WaitForSeconds(3);
            }
            else{
                break;
            }
        }
        isShooting = false;
        moving = true;
        yield return new WaitForSeconds(0);
    }

    //when boss dies, you win screen
    //when tree hits boss, switch sides + up speed
    //when boss hits tree, switch sides
}
