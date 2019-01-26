using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This should be a child of "tree"
[RequireComponent(typeof(Collider))]
public class TreeHitbox : MonoBehaviour {

  //Parent of TreeHitbox
  private GameController tree;

  //public float Weight => tree.Weight;
  public float Speed => tree.Speed;

  private void Awake() {
    tree = GetComponentInParent<GameController>();
  }
}
