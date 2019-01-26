using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {
#pragma warning disable 0649
  [SerializeField] private float _angleChange; //Degrees
  [SerializeField] private float pullTime; //Seconds
#pragma warning restore 0649

  //[Denominator] frames per second
  private float angleChangeTimeIncrement = 1f / 60f;

  //Instead of 0 to 180 degree rotation, it's limited to, as an example, 10 to 170 degrees
  private readonly float maxAngleOffset = 10f * Mathf.PI / 180f;

  //In radians
  private float AngleChange { get { return _angleChange * Mathf.PI / 180f; } }

  private Springy treeSlingshot;
  private Transform treeBase;

  public bool IsBeingPulled { get; private set; }

  //Degrees left (negative) or right (positive) of the y-axis (vertical)
  public float CurrentAngle {
    get {
      Vector3 currentPosition = treeSlingshot.SpringObj.position;
      if (currentPosition.x == 0) {
        return 90f;
      }
      return Mathf.Atan(currentPosition.y / currentPosition.x) * 180 / Mathf.PI;
    }
  }

  public float Speed { get { return treeSlingshot.Speed; } }

  private void Awake() {
    treeSlingshot = GetComponent<Springy>();
  }

  public void SetSpringForce(float force) {
    treeSlingshot.SetSpringForce(force);
  }

  public bool StartPulling() {
    if (IsBeingPulled) {
      Debug.LogError("Attempted to pull slingshot, but it's already being pulled");
      return false;
    }

    IsBeingPulled = true;
    treeSlingshot.PauseAndResetMomentum(true);

    Vector3 initialPosition = treeSlingshot.SpringObj.position;

    float initialX = treeSlingshot.SpringObj.position.x;

    float theta1 = Mathf.Atan(initialPosition.y / initialPosition.x);
    if (initialPosition.x == 0) {
      theta1 = Mathf.PI / 2;
    }

    float targetX;
    if (initialPosition.x < 0) {
      if (theta1 + AngleChange >= Mathf.PI - maxAngleOffset) {
        StopPulling();
        return false;
      }
      targetX = initialPosition.y / Mathf.Tan(theta1 + AngleChange);
    } else {
      if (theta1 - AngleChange <= maxAngleOffset) {
        StopPulling();
        return false;
      }
      targetX = initialPosition.y / Mathf.Tan(theta1 - AngleChange);
    }

    float deltaXPerUnitTime = (targetX - initialX) / pullTime * angleChangeTimeIncrement;

    StartCoroutine(PullRoutine(targetX, deltaXPerUnitTime));
    return true;
  }

  public bool StopPulling() {
    if (!IsBeingPulled) {
      return false;
    }

    IsBeingPulled = false;
    treeSlingshot.PauseAndResetMomentum(false);
    return true;
  }

  public void ToggleDrag(bool dragEnabled) {
    treeSlingshot.ToggleDrag(dragEnabled);
  }

  private IEnumerator PullRoutine(float targetX, float deltaXPerUnitTime) {
    while (IsBeingPulled) {

      bool slingshotMaxReached = Mathf.Abs(targetX - treeSlingshot.SpringObj.position.x) <= 0.1f;
      if (slingshotMaxReached) {
        StopPulling();
        yield break;
      }

      Vector3 initialPosition = treeSlingshot.SpringObj.position;

      Vector3 newPosition = new Vector3(initialPosition.x + deltaXPerUnitTime, initialPosition.y, initialPosition.z);
      treeSlingshot.SpringObj.position = newPosition;

      yield return new WaitForSeconds(angleChangeTimeIncrement);
    }
  }
}
