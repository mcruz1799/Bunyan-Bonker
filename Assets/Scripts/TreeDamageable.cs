using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class TreeDamageable : MonoBehaviour {
#pragma warning disable 0649
  [SerializeField] private int _maxHp;
#pragma warning restore 0649

  public int MaxHp { get { return _maxHp; } }
  public int CurrentHp { get; private set; }

  private OnDeath onDeathFunction = () => Debug.Log("No more HP!  You should probably do something in response to that.  ^.-");

  private void Awake() {
    CurrentHp = MaxHp;
  }

  private void OnCollisionEnter(Collision collision) {
    TreeController tree = collision.gameObject.GetComponentInParent<TreeController>();
    if (tree == null) {
      return;
    }

    //Debug.Log(collision.impulse);
    TakeDamage((int)Mathf.Abs(tree.Speed));
  }

  private void TakeDamage(int damage) {
    Debug.Log(damage);
    CurrentHp -= damage;
    if (CurrentHp <= 0) {
      CurrentHp = 0;
      onDeathFunction();
    }
  }

  public delegate void OnDeath();
  public void SetOnDeathBehavior(OnDeath onDeathFunction) {
    this.onDeathFunction = onDeathFunction;
  }
}
