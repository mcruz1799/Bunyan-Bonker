using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigglyTree : MonoBehaviour, ITree {
  private GameController gameController;

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