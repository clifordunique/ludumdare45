using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	[SerializeField] protected MenuUi _uiController;

	private void Awake() {
		_uiController.onStartClicked.AddListener(StartGame);
	}

	private static void StartGame() {
		SceneManager.LoadSceneAsync("Game");
	}
}