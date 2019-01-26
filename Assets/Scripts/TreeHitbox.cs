using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHitbox : MonoBehaviour {
  private GameController tree;

  public float Weight => tree.Weight;
  public float Speed => tree.Speed;

  private void Awake() {
    tree = GetComponentInParent<GameController>();
  }
}
