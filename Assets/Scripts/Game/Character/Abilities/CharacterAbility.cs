using System;
using UnityEngine;

[RequireComponent(typeof(CharacterSheet))]
public abstract class CharacterAbility : MonoBehaviour {
	[SerializeField] protected KeyCode _keyCode;

	public    KeyCode        keyCode => _keyCode;
	protected CharacterSheet sheet   { get; private set; }

	private void Awake() {
		sheet = GetComponent<CharacterSheet>();
	}

	private void Update() {
		PlayAbility(Input.GetKey(App.keyboardConfiguration.GetKeyboardKey(_keyCode)));
	}

	protected abstract void PlayAbility(bool keyDown);
}