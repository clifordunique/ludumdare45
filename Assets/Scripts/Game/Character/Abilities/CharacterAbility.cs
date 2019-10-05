using System;
using UnityEngine;

[RequireComponent(typeof(CharacterSheet))]
[RequireComponent(typeof(CharacterAnimator))]
public abstract class CharacterAbility : MonoBehaviour {
	[SerializeField] protected KeyCode _keyCode;
	[SerializeField] protected bool    _initiallyEnabled;

	public KeyCode keyCode          => _keyCode;
	public bool    initiallyEnabled => _initiallyEnabled;

	protected CharacterSheet    sheet    { get; private set; }
	protected CharacterAnimator animator { get; private set; }

	private void Awake() {
		sheet = GetComponent<CharacterSheet>();
		animator = GetComponent<CharacterAnimator>();
	}

	private void FixedUpdate() {
		PlayAbility(Input.GetKey(App.keyboardConfiguration.GetKeyboardKey(_keyCode)));
	}

	protected abstract void PlayAbility(bool keyDown);
}