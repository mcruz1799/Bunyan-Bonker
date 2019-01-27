using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMotion : MonoBehaviour {

#pragma warning disable 0649
  [SerializeField] private float speed;
  [SerializeField] private Vector3 leftEndpoint;
  [SerializeField] private Vector3 rightEndpoint;
#pragma warning restore 0649

  private IEnumerator MovementRoutine() {
    while (true) {
      //Randomly pick left to right or right to left
      Vector3 start, destination;
      if (Random.Range(0, 1) == 0) {
        start = leftEndpoint;
        destination = rightEndpoint;
      } else {
        start = rightEndpoint;
        destination = leftEndpoint;
      }

      float adjustedSpeed = speed * Random.Range(0.9f, 1.1f);

      transform.position = start;
      Vector3 directionVector = (destination - start).normalized;
      while ((transform.position - destination).sqrMagnitude > 0.1f) {
        transform.Translate(directionVector * adjustedSpeed * Time.deltaTime, Space.Self);
        yield return new WaitForSeconds(Time.deltaTime);
      }
    }
  }
}
