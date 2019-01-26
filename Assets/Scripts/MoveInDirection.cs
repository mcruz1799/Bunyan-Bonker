using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveInDirection : MonoBehaviour {
  [SerializeField] private Vector3 motion;

  private void Awake() {
    Rigidbody rb = GetComponent<Rigidbody>();
    rb.velocity = motion;
    rb.constraints = RigidbodyConstraints.FreezeAll;
  }
}
