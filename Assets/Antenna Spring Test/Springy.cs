using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] //Diverts the selection to this object
public class Springy : MonoBehaviour {
#pragma warning disable 0649
  [SerializeField] private Transform springTarget;
  [SerializeField] private Transform springObj;

  [Space(12)]
  [SerializeField] private float drag = 2.5f;//drag
  [SerializeField] private float springForce = 80.0f;//Spring

  [Space(12)]
  [SerializeField] private Transform GeoParent;

  [SerializeField] private Rigidbody SpringRB;

  [SerializeField] private bool pause = false;
#pragma warning restore 0649


  private Vector3 LocalDistance; //Distance between the two points
  private Vector3 LocalVelocity; //Velocity converted to local space

  private void Start() {
    SpringRB = springObj.GetComponent<Rigidbody>(); //Find the RigidBody component
    springObj.transform.parent = null; //Take the spring out of the hierarchy
  }

  private void Update() {
    //Pause and unpause with the space bar
    if (Input.GetKeyDown(KeyCode.Space)) {
      pause = !pause;
      SpringRB.gameObject.SetActive(!pause);
      Debug.Log(pause ? "Springy paused" : "Springy unpaused");
    }
  }

  private void FixedUpdate() {
    if (!pause) {
      //Sync the rotation
      SpringRB.transform.rotation = transform.rotation;

      //Calculate the distance between the two points
      LocalDistance = springTarget.InverseTransformDirection(springTarget.position - springObj.position);
      SpringRB.AddRelativeForce(LocalDistance * springForce); //Apply Spring

      //Calculate the local velocity of the springObj point
      LocalVelocity = springObj.InverseTransformDirection(SpringRB.velocity);
      SpringRB.AddRelativeForce(-LocalVelocity * drag); //Apply drag

      //Aim the visible geo at the spring target
      GeoParent.transform.LookAt(springObj.position, new Vector3(0, 0, 1));
    }
  }
}
