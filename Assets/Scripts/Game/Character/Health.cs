using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
	[SerializeField] private int _maxHealth;

	public UnityEvent onDead { get; } = new UnityEvent();

	private float health { get; set; }

	private void Awake() {
		health = _maxHealth;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer != LayerMask.NameToLayer("DamageSource")) return;
		if (health <= 0) return;
		health--;
		if (health <= 0) onDead.Invoke();
	}
}