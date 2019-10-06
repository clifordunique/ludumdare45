using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterSheet))]
[RequireComponent(typeof(Animator))]
public class DemonBrain : MonoBehaviour {
	private static int faceLeftAnimParam { get; } = Animator.StringToHash("FaceLeft");
	private static int walkingAnimParam  { get; } = Animator.StringToHash("Walking");
	private static int deadAnimParam     { get; } = Animator.StringToHash("Dead");

	[SerializeField] protected CharacterSheet      _characterSheet;
	[SerializeField] protected Animator            _animator;
	[SerializeField] protected SidePositionChecker _wallChecker;
	[SerializeField] protected SidePositionChecker _platformEndChecker;
	[SerializeField] protected float               _pauseTime = 2;

	private bool  directionRight { get; set; } = true;
	private float pauseStartTime { get; set; } = -100;

	private void Reset() {
		if (!_characterSheet) _characterSheet = GetComponent<CharacterSheet>();
		if (!_animator) _animator = GetComponent<Animator>();
	}

	private void Awake() {
		GetComponent<Health>().onDead.AddListener(HandleDeath);
	}

	private void HandleDeath() {
		_animator.SetTrigger(deadAnimParam);
		foreach (var thisColliders in GetComponentsInChildren<Collider2D>()) thisColliders.enabled = false;
		//AudioManager.Sfx.Play(_clipOnDead);
	}

	private void Update() {
		if (pauseStartTime + _pauseTime > Time.time) return;
		if (HasToStop()) {
			pauseStartTime = Time.time;
			_animator.SetBool(walkingAnimParam, false);
			directionRight = !directionRight;
			return;
		}
		_animator.SetBool(walkingAnimParam, true);
		_animator.SetBool(faceLeftAnimParam, !directionRight);
		transform.position += new Vector3((directionRight ? 1 : -1) * _characterSheet.speed * Time.deltaTime, 0, 0);
	}

	private bool HasToStop() {
		if (directionRight && (!_platformEndChecker.isRightValid || _wallChecker.isRightValid)) return true;
		if (!directionRight && (!_platformEndChecker.isLeftValid || _wallChecker.isLeftValid)) return true;
		return false;
	}
}