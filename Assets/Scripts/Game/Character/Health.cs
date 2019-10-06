using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Health : MonoBehaviour {
	[SerializeField] private int _maxHealth = 1;

	public UnityEvent onDead { get; } = new UnityEvent();

	private float health { get; set; }

	private void Awake() {
		health = _maxHealth;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer != LayerMask.NameToLayer("DamageSource")) return;
		if (health <= 0) return;
		health--;
		if (health > 0) return;
		onDead.Invoke();
		var position = transform.position;
		transform.position = position.With(y: position.y.RoundDown() + .5f);
		var rb2 = GetComponent<Rigidbody2D>();
		rb2.gravityScale = 0;
		rb2.velocity = Vector2.zero;
		GetComponent<Collider2D>().enabled = false;
	}
}