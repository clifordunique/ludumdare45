using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterJump : CharacterAbility {
	[SerializeField] protected Rigidbody2D         _rigidBody2D;
	[SerializeField] protected PositionChecker     _groundPositionChecker;
	[SerializeField] protected SidePositionChecker _wallPositionChecker;
	[SerializeField] protected float               _cooldown = .5f;
	[SerializeField] protected AudioClip           _jumpAudioClip;

	private float cooldownCounter { get; set; } = -100;

	private void Reset() {
		if (!_rigidBody2D) _rigidBody2D = GetComponent<Rigidbody2D>();
	}

	protected override void PlayAbility(bool keyDown) {
		animator.SetOnGround(_groundPositionChecker.isValid);
		if (!keyDown) return;
		if (cooldownCounter + _cooldown > Time.time) return;
		if (!_groundPositionChecker.isValid && !_wallPositionChecker.isValid) return;
		var direction = _groundPositionChecker.isValid ? Vector2.up : _wallPositionChecker.isLeftValid ? Vector2.right : Vector2.left;
		_rigidBody2D.AddForce(direction * sheet.jumpForce);
		cooldownCounter = Time.time;
		AudioManager.Sfx.Play(_jumpAudioClip);
	}
}