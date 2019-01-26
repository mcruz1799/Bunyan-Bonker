using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hidden.WigglyTreeControls {
  public class GameController : MonoBehaviour {
#pragma warning disable 0649
    [SerializeField] private TreeController treeController;
#pragma warning restore 0649

    public float MaxPower { get; } = 100f; //Max Speed is about 400
    public float MinPower { get; } = 10f; //Max Speed is about 100
    public float Power { get; private set; }

    //public float MaxWeight { get; }
    public float Weight { get; private set; } = 100f;
    public float Speed { get { return treeController.Speed; } }

    public float Angle { get { return treeController.Angle; } }

    private bool canPull = true;
    private float pullCooldownTime = 2f;

    private int xVelocityDirection = 0;

    private void Start() {
      Power = MinPower;
      treeController.SetSpringForce(Power);
    }

    private int sideSwitchCounter;
    private int switchesBeforeDragKicksIn = 4;
    private void Update() {
      //Pull/release the tree with the space bar
      if (canPull && Input.GetKeyDown(KeyCode.Space)) {
        bool startPullingSucceeded = treeController.StartPulling();
        if (!startPullingSucceeded) {
          //TODO: Play a sound to let the player know they can't pull yet
        }
        sideSwitchCounter = 0;
        treeController.ToggleDrag(false);
        StartCoroutine(PullCooldownRoutine(pullCooldownTime));

      } else if (Input.GetKeyUp(KeyCode.Space)) {
        treeController.StopPulling();
      }

      int newXVelocityDirection = System.Math.Sign(treeController.Speed);
      if (newXVelocityDirection != xVelocityDirection && !treeController.IsBeingPulled && Mathf.Abs(treeController.Speed) >= 0.01f) {
        if (sideSwitchCounter == switchesBeforeDragKicksIn) {
          treeController.ToggleDrag(true);
        }
        sideSwitchCounter += 1;
      }
      xVelocityDirection = newXVelocityDirection;
    }

    public bool UpgradePower() {
      if (Power == MaxPower) {
        return false;
      }
      Power = Mathf.Min(MaxPower, Power + 10f);
      treeController.SetSpringForce(Power);
      return true;
    }

    public bool UpgradeSize() {
      if (Power - 5f < MinPower) {
        return false;
      }
      Weight += 0.1f;
      Power -= 5f;
      return true;
    }

    //Must wait for some amount of time between pulls
    private IEnumerator PullCooldownRoutine(float cooldownTime) {
      canPull = false;
      yield return new WaitForSeconds(cooldownTime);
      canPull = true;
    }
  }
}