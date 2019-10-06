using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StateButton : MonoBehaviour {
	[SerializeField] protected Button _button;
	[SerializeField] protected Sprite _inactiveSprite;
	[SerializeField] protected Sprite _activeSprite;
	[SerializeField] protected bool   _active;

	public bool active {
		get => _active;
		set {
			_active = value;
			_button.image.sprite = _active ? _activeSprite : _inactiveSprite;
		}
	}

	public UnityEvent onClick => _button.onClick;

	private void Reset() {
		if (!_button) _button = GetComponent<Button>();
	}

	private void OnValidate() {
		active = _active;
	}
}