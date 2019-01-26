using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase] //Diverts the selection to this object
public class Springy : MonoBehaviour {
#pragma warning disable 0649
  [SerializeField] private Transform springTarget;
  [SerializeField] private Transform springObj;
  public Transform SpringObj { get { return springObj; } }

  [Space(12)]
  [SerializeField] private float drag;
  [SerializeField] private float springForce;

  [Space(12)]
  [SerializeField] private Transform GeoParent;

  private Rigidbody SpringRB;
#pragma warning restore 0649

  private Vector3 localDistance; //Distance between the two points
  private Vector3 localVelocity; //Velocity converted to local space

  private bool isPaused = false;
  private bool dragEnabled = true;

  //Velocity that the spring (i.e. the end of the object) is moving at
  public Vector3 Velocity { get { return SpringRB.velocity; } }

  private void Start() {
    SpringRB = springObj.GetComponent<Rigidbody>(); //Find the RigidBody component
    springObj.transform.parent = null; //Take the spring out of the hierarchy
  }

  private void FixedUpdate() {
    if (!isPaused) {
      //Sync the rotation
      SpringRB.transform.rotation = transform.rotation;

      //Calculate the distance between the two points
      localDistance = springTarget.InverseTransformDirection(springTarget.position - springObj.position);
      SpringRB.AddRelativeForce(localDistance * springForce); //Apply Spring

      //Calculate the local velocity of the springObj point
      if (dragEnabled) {
        localVelocity = springObj.InverseTransformDirection(SpringRB.velocity);
        SpringRB.AddRelativeForce(-localVelocity * drag); //Apply drag
      }
    }
    //Aim the visible geo at the spring target
    GeoParent.transform.LookAt(springObj.position, new Vector3(0, 0, 1));
  }

  public void PauseAndResetMomentum(bool isPaused) {
    this.isPaused = !this.isPaused;
    SpringRB.gameObject.SetActive(!this.isPaused);
    SpringRB.velocity = Vector3.zero;
  }

  public void ToggleDrag(bool dragEnabled) {
    this.dragEnabled = dragEnabled;
  }
}
