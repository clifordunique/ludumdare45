using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterAnimator))]
public class CharacterBrain : MonoBehaviour {
	public class AbilityUnlockedEvent : UnityEvent<KeyCode> { }

	[SerializeField] protected PositionChecker     _groundPositionChecker;
	[SerializeField] protected SidePositionChecker _wallPositionChecker;

	private CharacterAnimator                     animator     { get; set; }
	private Dictionary<KeyCode, CharacterAbility> allAbilities { get; } = new Dictionary<KeyCode, CharacterAbility>();

	public AbilityUnlockedEvent onAbilityUnlocked { get; } = new AbilityUnlockedEvent();

	private void Awake() {
		animator = GetComponent<CharacterAnimator>();
		GetComponent<Health>().onDead.AddListener(HandleDeath);
		foreach (var ability in GetComponents<CharacterAbility>()) {
			allAbilities.Add(ability.keyCode, ability);
			ability.enabled = ability.initiallyEnabled;
		}
	}

	private void HandleDeath() {
		Debug.Log("Dead");
		animator.SetDead();
		foreach (var ability in GetComponents<CharacterAbility>()) ability.enabled = false;
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (!other.TryGetComponent<AbilityUnlocker>(out var abilityUnlocker)) return;
		UnlockAbility(abilityUnlocker.keyCode);
		abilityUnlocker.Consume();
	}

	private void UnlockAbility(KeyCode keyCode) {
		if (!allAbilities.ContainsKey(keyCode)) {
			Debug.LogWarning($"No ability matching the key {keyCode}");
			return;
		}
		var ability = allAbilities[keyCode];
		if (ability.enabled) {
			Debug.LogWarning($"Ability of type {ability.GetType().Name} was already unlocked.");
			return;
		}
		ability.enabled = true;
		onAbilityUnlocked.Invoke(keyCode);
	}

	private void Update() {
		animator.SetOnGround(_groundPositionChecker.isValid);
		animator.SetGripped(!_groundPositionChecker.isValid && _wallPositionChecker.isValid);
	}
}