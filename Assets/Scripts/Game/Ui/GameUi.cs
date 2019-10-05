using UnityEngine;

public class GameUi : MonoBehaviour {
	[SerializeField] protected CharacterBrain _character;

	private void Awake() {
		_character.onAbilityUnlocked.AddListener(t => Debug.Log(t));
	}
}