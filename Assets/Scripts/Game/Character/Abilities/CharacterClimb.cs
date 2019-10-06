using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterClimb : CharacterAbility {
	[SerializeField] protected Rigidbody2D         _rigidBody2D;
	[SerializeField] protected SidePositionChecker _wallPositionChecker;

	[Header("Audios")] [SerializeField] protected AudioClip _climbAudio;
	[SerializeField]                    protected float     _delayBetweenClimbAudios = 2;

	private float lastAudio { get; set; } = -100;

	private void Reset() {
		if (!_rigidBody2D) _rigidBody2D = GetComponent<Rigidbody2D>();
	}

	protected override void PlayAbility(bool keyDown) {
		animator.SetClimbing(_wallPositionChecker.isValid && keyDown);
		_rigidBody2D.gravityScale = _wallPositionChecker.isValid ? 0 : 1;
		if (!_wallPositionChecker.isValid) return;
		if (_rigidBody2D.velocity != Vector2.zero) _rigidBody2D.velocity = Vector2.zero;
		if (!_wallPositionChecker.isLeftValid || !_wallPositionChecker.isRightValid) animator.SetFaceLeft(_wallPositionChecker.isLeftValid);
		if (!keyDown) return;
		animator.SetGripped(_wallPositionChecker.isValid);
		transform.position += Time.deltaTime * sheet.speed * Vector3.up;
		TryPlayClimbAudio();
	}

	private void TryPlayClimbAudio() {
		if (!(lastAudio + _delayBetweenClimbAudios < Time.time)) return;
		AudioManager.Sfx.Play(_climbAudio);
		lastAudio = Time.time;
	}
}