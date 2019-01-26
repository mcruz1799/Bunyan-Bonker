using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMechanics : MonoBehaviour
{
    public float movGen = 0.02f;
    float speed = 0.02f;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        GetComponent<TreeDamageable>().SetOnDeathBehavior(() => Destroy(gameObject));
        speed = movGen; //* level;
        if(this.transform.position.x > 0) speed = -speed;
    }

   void Update()
    {
        //assuming we're in the x-y plane, move in x direction
        this.transform.Translate(new Vector3(speed, 0, 0));

    }

    public void SwitchSides(){
        //move to opposite side [animation?]
        this.transform.position = new Vector3(startPos.x * -1, startPos.y,
            startPos.z);
        speed = -speed;
        startPos = new Vector3(startPos.x * -1, startPos.y, startPos.z);
    }

    //when boss dies, you win screen
    //when tree hits boss, switch sides
    //when boss hits tree, switch sides
}
