using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {
	[SerializeField] protected GameUi         _uiController;
	[SerializeField] protected CageBreaker    _cageBreaker;
	[SerializeField] protected CharacterBrain _character;
	[SerializeField] protected string         _nextLevelName;

	[Header("Transitions")] [SerializeField] protected TransitionScript _introScript;
	[SerializeField]                         protected TransitionScript _cageScript;
	[SerializeField]                         protected float            _cageScriptCooldown = 4;
	[SerializeField]                         protected TransitionScript _outroScript;

	private bool lastLevel  => string.IsNullOrEmpty(_nextLevelName);
	private bool readyToEnd { get; set; }

	private void Awake() {
		TransitionManager.Play(_introScript, StartLevel);
		_character.onExitReached.AddListener(HandleEndLevel);
		_character.GetComponent<Health>().onDead.AddListener(HandleDeath);
		if (lastLevel) _character.onAbilityUnlocked.AddListener(t => readyToEnd |= t == KeyCode.X);
	}

	private void StartLevel() {
		_uiController.StartLevel();
		_cageBreaker.Break();
		if (_cageScript) StartCoroutine(PlayCageScript());
	}

	private IEnumerator PlayCageScript() {
		yield return new WaitForSeconds(_cageScriptCooldown);
		TransitionManager.Play(_cageScript);
	}

	private void HandleEndLevel() {
		_uiController.EndLevel();
		TransitionManager.Play(_outroScript, () => SceneManager.LoadSceneAsync(_nextLevelName));
	}

	private void HandleDeath() {
		if (lastLevel && readyToEnd) {
			TransitionManager.Play(_outroScript, () => SceneManager.LoadSceneAsync("GameOver"));
		}
		else {
			TransitionManager.PlayDeadOutro(() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name));
		}
	}
}