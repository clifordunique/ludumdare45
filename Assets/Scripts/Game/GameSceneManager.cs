using System.Collections;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {
	[SerializeField] protected GameUi         _uiController;
	[SerializeField] protected CageBreaker    _cageBreaker;
	[SerializeField] protected CharacterBrain _character;

	private void Awake() {
		StartCoroutine(DoStartLevel());
		_character.onExitReached.AddListener(HandleEndLevel);
	}

	private void HandleEndLevel() => StartCoroutine(DoEndLevel());

	private IEnumerator DoStartLevel() {
		// TODO start level dialogue
		yield return null;
		_uiController.StartLevel();
		_cageBreaker.Break();
	}

	private IEnumerator DoEndLevel() {
		_uiController.EndLevel();
		yield return null;
	}
}