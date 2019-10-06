using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	[SerializeField]                        protected MenuUi           _uiController;
	[Header("Transition")] [SerializeField] private   TransitionScript _globalIntro;

	private void Awake() {
		TransitionManager.ClearTransitionsToSkip();

		_uiController.onStartClicked.AddListener(StartGame);
		_uiController.onKeyboardLayoutChange.AddListener(SetKeyboardLayout);
		_uiController.onMusicVolumeChanged.AddListener(t => AudioManager.Music.volume = t);
		_uiController.onSfxVolumeChanged.AddListener(t => AudioManager.Sfx.volume = t);
		_uiController.onVoiceVolumeChanged.AddListener(t => AudioManager.Voice.volume = t);
	}

	private void Start() {
		_uiController.SetActiveKeyboardLayout(App.keyboardConfiguration);
		_uiController.SetAudioVolumes(AudioManager.Music.volume, AudioManager.Sfx.volume, AudioManager.Voice.volume);
	}

	private void SetKeyboardLayout(KeyboardConfiguration config) {
		App.keyboardConfiguration = config;
		_uiController.SetActiveKeyboardLayout(config);
	}

	private void StartGame() {
		_uiController.Hide();
		if (_globalIntro) TransitionManager.Play(_globalIntro, () => SceneManager.LoadSceneAsync("Level0"));
		else SceneManager.LoadSceneAsync("Level0");
	}
}