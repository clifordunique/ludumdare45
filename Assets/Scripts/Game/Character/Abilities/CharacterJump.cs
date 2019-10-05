using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterJump : CharacterAbility {
	[SerializeField] protected Rigidbody2D     _rigidBody2D;
	[SerializeField] protected PositionChecker _groundPositionChecker;
	[SerializeField] protected float           _cooldown = .5f;

	private float cooldownCounter { get; set; } = -100;

	private void Reset() {
		if (!_rigidBody2D) _rigidBody2D = GetComponent<Rigidbody2D>();
	}

	protected override void PlayAbility(bool keyDown) {
		if (!keyDown) return;
		if (cooldownCounter + _cooldown > Time.time) return;
		if (!_groundPositionChecker.isValid) return;
		_rigidBody2D.AddForce(Vector2.up * sheet.jumpForce);
		cooldownCounter = Time.time;
	}
}