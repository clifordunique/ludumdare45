using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	[SerializeField]                        protected MenuUi           _uiController;
	[Header("Transition")] [SerializeField] private   TransitionScript _globalIntro;

	private void Awake() {
		TransitionManager.ClearTransitionsToSkip();
		_uiController.onStartClicked.AddListener(StartGame);
	}

	private void StartGame() {
		_uiController.Hide();
		if (_globalIntro) TransitionManager.Play(_globalIntro, () => SceneManager.LoadSceneAsync("Level0"));
		else SceneManager.LoadSceneAsync("Level0");
	}
}