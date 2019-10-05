using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {
	[SerializeField] protected GameUi         _uiController;
	[SerializeField] protected CageBreaker    _cageBreaker;
	[SerializeField] protected CharacterBrain _character;
	[SerializeField] protected string         _nextLevelName;

	[Header("Transitions")] [SerializeField] private TransitionScript _introScript;
	[SerializeField]                         private TransitionScript _outroScript;

	private void Awake() {
		if (_introScript) TransitionManager.Play(_introScript, StartLevel);
		else StartLevel();
		_character.onExitReached.AddListener(HandleEndLevel);
	}

	private void StartLevel() {
		_uiController.StartLevel();
		_cageBreaker.Break();
	}

	private void HandleEndLevel() {
		_uiController.EndLevel();
		if (_outroScript) TransitionManager.Play(_outroScript, LoadNextLevel);
		else LoadNextLevel();
	}

	private void LoadNextLevel() => SceneManager.LoadSceneAsync(_nextLevelName);
}