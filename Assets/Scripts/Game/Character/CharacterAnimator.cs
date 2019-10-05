using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour {
	private static int faceLeftAnimParam { get; } = Animator.StringToHash("FaceLeft");
	private static int onGroundAnimParam { get; } = Animator.StringToHash("OnGround");
	private static int grippedAnimParam  { get; } = Animator.StringToHash("Gripped");
	private static int walkingAnimParam  { get; } = Animator.StringToHash("Walking");
	private static int climbingAnimParam { get; } = Animator.StringToHash("Climbing");
	private static int deadAnimParam     { get; } = Animator.StringToHash("Dead");

	[SerializeField] protected Animator _animator;

	public bool walkingRight { get; set; }
	public bool walkingLeft  { get; set; }

	private void Reset() {
		if (!_animator) _animator = GetComponent<Animator>();
	}

	public void SetFaceLeft(bool left) => _animator.SetBool(faceLeftAnimParam, left);
	public void SetOnGround(bool onGround) => _animator.SetBool(onGroundAnimParam, onGround);
	public void SetGripped(bool gripped) => _animator.SetBool(grippedAnimParam, gripped);

	private void Update() {
		_animator.SetBool(walkingAnimParam, walkingLeft != walkingRight);
	}

	public void SetDead() => _animator.SetTrigger(deadAnimParam);

	public void SetClimbing(bool climbing) => _animator.SetBool(climbingAnimParam, climbing);
}