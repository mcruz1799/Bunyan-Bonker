using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hidden.WigglyTreeControls;

public interface IWigglyTree {
  //Higher Speed increases the damage you deal
  float Speed { get; }

  float Angle { get; }

  //Higher Power means the tree wiggles faster
  //Speed will be higher, but it's harder to time your wiggles
  float Power { get; }

  //Higher Weight increases the damage you deal
  float Weight { get; }

  //Increases Power
  bool UpgradePower();

  //Increases Weight, decreases Power, and enlarges the tree sprite
  bool UpgradeSize();
}

public class WigglyTree : MonoBehaviour, IWigglyTree {
  private GameController gameController;

  private void Awake() {
    gameController = GetComponent<GameController>();
  }

  public float Speed => gameController.Speed;
  public float Angle => gameController.Angle;
  public float Power => gameController.Power;
  public float Weight => gameController.Weight;

  public bool UpgradePower() {
    return gameController.UpgradePower();
  }

  public bool UpgradeSize() {
    return gameController.UpgradeSize();
  }
}