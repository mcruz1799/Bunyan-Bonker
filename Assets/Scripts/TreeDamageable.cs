using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    TreeHitbox tree = collision.gameObject.GetComponentInParent<TreeHitbox>();
    if (tree == null) {
      return;
    }

    TakeDamage((int)Mathf.Abs(tree.Speed));
  }

  private void TakeDamage(int damage) {
    if (CurrentHp <= 0) {
      return;
    }
    CurrentHp -= damage;
    if (damage > 0) {
      onDamaged.Invoke();
    }
    if (CurrentHp <= 0) {
      CurrentHp = 0;
      onDeathFunction();
    }
  }

  public delegate void OnDeath();
  public void SetOnDeathBehavior(OnDeath onDeathFunction) {
    this.onDeathFunction = onDeathFunction;
  }

  private UnityEvent onDamaged = new UnityEvent();
  public void AddOnDamagedListener(UnityAction onDamaged) {
    this.onDamaged.AddListener(onDamaged);
  }
}
