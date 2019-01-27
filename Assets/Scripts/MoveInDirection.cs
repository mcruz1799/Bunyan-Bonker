using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveInDirection : MonoBehaviour {
#pragma warning disable 0649
  [SerializeField] private Vector3 motion;
#pragma warning restore 0649

  private void Awake() {
    Rigidbody rb = GetComponent<Rigidbody>();
    rb.velocity = motion;
    rb.constraints = RigidbodyConstraints.FreezeAll;
  }
}
