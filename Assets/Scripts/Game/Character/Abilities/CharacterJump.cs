using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterJump : CharacterAbility {
	[SerializeField] protected Rigidbody2D         _rigidBody2D;
	[SerializeField] protected PositionChecker     _groundPositionChecker;
	[SerializeField] protected SidePositionChecker _wallPositionChecker;
	[SerializeField] protected AudioClip           _jumpAudioClip;

	private bool justJumped { get; set; }

	private void Reset() {
		if (!_rigidBody2D) _rigidBody2D = GetComponent<Rigidbody2D>();
	}

	protected override void PlayAbility(bool keyDown) {
		if (!keyDown) {
			justJumped = false;
			return;
		}
		if (justJumped) return;
		if (!_groundPositionChecker.isValid && !_wallPositionChecker.isValid) return;
		var direction = _groundPositionChecker.isValid ? Vector2.up : _wallPositionChecker.isLeftValid ? Vector2.right : Vector2.left;
		_rigidBody2D.AddForce(direction * sheet.jumpForce);
		justJumped = true;
		AudioManager.Sfx.Play(_jumpAudioClip);
	}
}