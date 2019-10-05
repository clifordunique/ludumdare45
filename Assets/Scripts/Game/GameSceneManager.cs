using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {
	[SerializeField] protected GameUi         _uiController;
	[SerializeField] protected CageBreaker    _cageBreaker;
	[SerializeField] protected CharacterBrain _character;
	[SerializeField] protected string         _nextLevelName;

	[Header("Transitions")] [SerializeField] private TransitionScript _introScript;
	[SerializeField]                         private TransitionScript _outroScript;

	private bool lastLevel => string.IsNullOrEmpty(_nextLevelName);

	private void Awake() {
		if (_introScript && !TransitionManager.IsToSkip(_introScript)) TransitionManager.Play(_introScript, StartLevel);
		else StartLevel();
		_character.onExitReached.AddListener(HandleEndLevel);
		_character.GetComponent<Health>().onDead.AddListener(HandleDeath);
	}

	private void StartLevel() {
		if (_introScript) TransitionManager.SkipInTheFuture(_introScript);
		_uiController.StartLevel();
		_cageBreaker.Break();
	}

	private void HandleEndLevel() {
		_uiController.EndLevel();
		if (_outroScript && !TransitionManager.IsToSkip(_outroScript)) TransitionManager.Play(_outroScript, LoadNextLevel);
		else LoadNextLevel();
	}

	private void LoadNextLevel() {
		if (_outroScript) TransitionManager.SkipInTheFuture(_outroScript);
		SceneManager.LoadSceneAsync(_nextLevelName);
	}

	private void HandleDeath() {
		if (lastLevel) {
			TransitionManager.Play(_outroScript, () => SceneManager.LoadSceneAsync("GameOver"));
		}
		else {
			TransitionManager.PlayDeadOutro( () => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name));
		}
	}
}