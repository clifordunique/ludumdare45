using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterAnimator))]
public class CharacterBrain : MonoBehaviour {
	public class AbilityUnlockedEvent : UnityEvent<KeyCode> { }

	[SerializeField] protected PositionChecker     _groundPositionChecker;
	[SerializeField] protected SidePositionChecker _wallPositionChecker;
	[SerializeField] protected AudioClip           _clipOnUnlockAbility;
	[SerializeField] protected AudioClip           _clipOnDead;
	[SerializeField] protected AudioClip           _clipOnExit;

	private CharacterAnimator                     animator     { get; set; }
	private Dictionary<KeyCode, CharacterAbility> allAbilities { get; } = new Dictionary<KeyCode, CharacterAbility>();

	private bool exited { get; set; }

	public AbilityUnlockedEvent onAbilityUnlocked { get; } = new AbilityUnlockedEvent();
	public UnityEvent           onExitReached     { get; } = new UnityEvent();

	private void Awake() {
		animator = GetComponent<CharacterAnimator>();
		GetComponent<Health>().onDead.AddListener(HandleDeath);
		foreach (var ability in GetComponents<CharacterAbility>()) {
			allAbilities.Add(ability.keyCode, ability);
			ability.enabled = ability.initiallyEnabled;
		}
	}

	private void HandleDeath() {
		animator.SetDead();
		foreach (var ability in GetComponents<CharacterAbility>()) ability.enabled = false;
		AudioManager.Sfx.Play(_clipOnDead);
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.TryGetComponent<AbilityUnlocker>(out var abilityUnlocker)) {
			UnlockAbility(abilityUnlocker.keyCode);
			abilityUnlocker.Consume();
		}
		else if (!exited && other.gameObject.layer == LayerMask.NameToLayer("Exit")) {
			exited = true;
			animator.SetExit();
			foreach (var ability in GetComponents<CharacterAbility>()) ability.enabled = false;
			AudioManager.Sfx.Play(_clipOnExit);
			onExitReached.Invoke();
		}
	}

	private void UnlockAbility(KeyCode keyCode) {
		if (!allAbilities.ContainsKey(keyCode)) {
			Debug.LogWarning($"No ability matching the key {keyCode}");
			return;
		}
		var ability = allAbilities[keyCode];
		if (ability.enabled) return;
		ability.enabled = true;
		AudioManager.Sfx.Play(_clipOnUnlockAbility);
		onAbilityUnlocked.Invoke(keyCode);
	}

	private void Update() {
		animator.SetOnGround(_groundPositionChecker.isValid);
		animator.SetGripped(!_groundPositionChecker.isValid && _wallPositionChecker.isValid);
	}
}