using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StateButton))]
public class KeyboardLayoutButton : MonoBehaviour {
	public class KeyboardLayoutButtonClicked : UnityEvent<KeyboardConfiguration> { }

	[SerializeField] protected StateButton           _button;
	[SerializeField] protected KeyboardConfiguration _keyboardLayout;

	public KeyboardLayoutButtonClicked onClick { get; } = new KeyboardLayoutButtonClicked();

	public bool active {
		get => _button.active;
		set => _button.active = value;
	}

	public KeyboardConfiguration layout => _keyboardLayout;

	private void Reset() {
		if (!_button) _button = GetComponent<StateButton>();
	}

	private void Awake() {
		_button.onClick.AddListener(() => onClick.Invoke(_keyboardLayout));
	}
}