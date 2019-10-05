using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TransitionUi : MonoBehaviour {
	private static int visibleAnimParam { get; } = Animator.StringToHash("Visible");

	[SerializeField] protected Animator       _animator;
	[SerializeField] protected TMPro.TMP_Text _subtitle;

	public string subtitleText {
		get => _subtitle.text;
		set => _subtitle.text = value;
	}

	private void Reset() {
		if (!_animator) _animator = GetComponent<Animator>();
	}

	public void SetVisible(bool visible) {
		_animator.SetBool(visibleAnimParam, visible);
	}
}