using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
#pragma warning disable 0649
  [SerializeField] private TreeController treeController;
#pragma warning restore 0649

  private bool canPull = true;
  private float pullCooldownTime = 2f;

  private int xVelocityDirection = 0;

  private void Update() {
    //Pull/release the tree with the space bar
    if (canPull && Input.GetKeyDown(KeyCode.Space)) {
      bool startPullingSucceeded = treeController.StartPulling();
      if (!startPullingSucceeded) {
        //TODO: Play a sound to let the player know they can't pull yet
      }
      StartCoroutine(PullCooldownRoutine(pullCooldownTime));

    } else if (Input.GetKeyUp(KeyCode.Space)) {
      treeController.StopPulling();
    }

    int newXVelocityDirection = System.Math.Sign(treeController.Velocity.x);
    if (newXVelocityDirection != xVelocityDirection && !treeController.IsBeingPulled && treeController.Velocity.sqrMagnitude >= 0.01f) {
      Debug.Log("New direction!");
    }
    xVelocityDirection = newXVelocityDirection;
  }

  //Must wait for some amount of time between pulls
  private IEnumerator PullCooldownRoutine(float cooldownTime) {
    canPull = false;
    yield return new WaitForSeconds(cooldownTime);
    canPull = true;
  }
}
