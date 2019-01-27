using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScenery : MonoBehaviour {

#pragma warning disable 0649
  [SerializeField] private float speed;
  [SerializeField] private Vector3 leftEndpoint;
  [SerializeField] private Vector3 rightEndpoint;
  [SerializeField] private List<Sprite> cloudSprites;
#pragma warning restore 0649

  private void Start() {
    StartCoroutine(MovementRoutine());
  }

  private IEnumerator MovementRoutine() {
    while (true) {
      GetComponent<SpriteRenderer>().sprite = cloudSprites[Random.Range(0, cloudSprites.Count)];

      //Randomly pick left to right or right to left
      Vector3 start, destination;
      if (Random.Range(0, 2) == 0) {
        start = leftEndpoint;
        destination = rightEndpoint;
      } else {
        start = rightEndpoint;
        destination = leftEndpoint;
      }

      transform.position = start;
      yield return new WaitForSeconds(Random.Range(1f, 5f));

      float adjustedSpeed = speed * Random.Range(0.9f, 1.1f);
      Vector3 directionVector = (destination - start).normalized;
      while ((transform.position - destination).sqrMagnitude > 0.1f) {
        transform.Translate(directionVector * adjustedSpeed * Time.deltaTime, Space.Self);
        yield return new WaitForSeconds(Time.deltaTime);
      }
    }
  }
}
