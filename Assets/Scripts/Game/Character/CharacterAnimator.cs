using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour {
	private static int faceLeftAnimParam { get; } = Animator.StringToHash("FaceLeft");
	private static int inTheAirAnimParam { get; } = Animator.StringToHash("InTheAir");
	private static int walkingAnimParam  { get; } = Animator.StringToHash("Walking");

	[SerializeField] protected Animator _animator;

	public bool walkingRight { get; set; }
	public bool walkingLeft  { get; set; }

	private void Reset() {
		if (!_animator) _animator = GetComponent<Animator>();
	}

	public void SetFaceLeft(bool left) => _animator.SetBool(faceLeftAnimParam, left);
	public void SetInThAir(bool inTheAir) => _animator.SetBool(inTheAirAnimParam, inTheAir);

	private void Update() {
		_animator.SetBool(walkingAnimParam, walkingLeft != walkingRight);
	}
}