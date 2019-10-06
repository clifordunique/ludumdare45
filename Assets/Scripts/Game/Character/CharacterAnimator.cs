using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour {
	private static int faceLeftAnimParam { get; } = Animator.StringToHash("FaceLeft");
	private static int onGroundAnimParam { get; } = Animator.StringToHash("OnGround");
	private static int grippedAnimParam  { get; } = Animator.StringToHash("Gripped");
	private static int walkingAnimParam  { get; } = Animator.StringToHash("Walking");
	private static int climbingAnimParam { get; } = Animator.StringToHash("Climbing");
	private static int deadAnimParam     { get; } = Animator.StringToHash("Dead");
	private static int exitAnimParam     { get; } = Animator.StringToHash("Exit");
	private static int shootAnimParam    { get; } = Animator.StringToHash("Shoot");

	[SerializeField] protected Animator _animator;

	public bool walkingRight { get; set; }
	public bool walkingLeft  { get; set; }

	public bool faceLeft {
		get => _animator.GetBool(faceLeftAnimParam);
		set => _animator.SetBool(faceLeftAnimParam, value);
	}

	public bool gripped {
		get => _animator.GetBool(grippedAnimParam);
		set => _animator.SetBool(grippedAnimParam, value);
	}

	public bool onGround {
		get => _animator.GetBool(onGroundAnimParam);
		set => _animator.SetBool(onGroundAnimParam, value);
	}

	public bool climbing {
		get => _animator.GetBool(climbingAnimParam);
		set => _animator.SetBool(climbingAnimParam, value);
	}

	private void Reset() {
		if (!_animator) _animator = GetComponent<Animator>();
	}

	private void Update() {
		_animator.SetBool(walkingAnimParam, walkingLeft != walkingRight);
	}

	public void SetDead() => _animator.SetTrigger(deadAnimParam);
	public void SetExit() => _animator.SetTrigger(exitAnimParam);
	public void SetShoot() => _animator.SetTrigger(shootAnimParam);
}