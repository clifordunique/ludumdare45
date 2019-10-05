using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AbilityUnlocker : MonoBehaviour {
	private static readonly int consumeAnimParam = Animator.StringToHash("Consume");

	[SerializeField] protected Animator       _animator;
	[SerializeField] protected KeyCode        _keyCode;
	[SerializeField] protected TMPro.TMP_Text _visualText;

	public KeyCode keyCode => _keyCode;

	private void Reset() {
		if (!_animator) _animator = GetComponent<Animator>();
		if (!_visualText) _visualText = GetComponentInChildren<TMPro.TMP_Text>();
	}

	private void Awake() {
		if (_visualText) _visualText.text = App.keyboardConfiguration.GetKeyboardKey(_keyCode).ToString();
	}

	public void Consume() {
		GetComponent<Collider2D>().enabled = false;
		_animator.SetTrigger(consumeAnimParam);
	}

	public void Destroy() {
		Destroy(gameObject);
	}
}