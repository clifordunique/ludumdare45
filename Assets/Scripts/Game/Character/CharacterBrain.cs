using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterBrain : MonoBehaviour {
	public class AbilityUnlockedEvent : UnityEvent<KeyCode> { }

	private Dictionary<KeyCode, CharacterAbility> allAbilities      { get; } = new Dictionary<KeyCode, CharacterAbility>();
	private HashSet<CharacterAbility>             unlockedAbilities { get; } = new HashSet<CharacterAbility>();

	public AbilityUnlockedEvent onAbilityUnlocked { get; } = new AbilityUnlockedEvent();

	private void Awake() {
		foreach (var ability in GetComponents<CharacterAbility>()) {
			allAbilities.Add(ability.keyCode, ability);
			ability.enabled = false;
		}
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
		if (unlockedAbilities.Contains(ability)) {
			Debug.LogWarning($"Ability of type {ability.GetType().Name} was already unlocked.");
			return;
		}
		unlockedAbilities.Add(ability);
		ability.enabled = true;
		onAbilityUnlocked.Invoke(keyCode);
	}
}