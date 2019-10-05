using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GameUi : MonoBehaviour {
	private static int blackScreenAnimParam { get; } = Animator.StringToHash("BlackScreen");

	[SerializeField] protected Animator _animator;

	[SerializeField] protected CharacterBrain _character;

	private void Reset() {
		if (!_animator) _animator = GetComponent<Animator>();
	}

	private void Awake() {
		_character.onAbilityUnlocked.AddListener(t => Debug.Log(t));
	}

	public void EndLevel() => _animator.SetBool(blackScreenAnimParam, true);
	public void StartLevel() => _animator.SetBool(blackScreenAnimParam, false);
}