using System;
using System.Collections;
using UnityEngine;

public class TransitionManager : MonoBehaviour {
	public static TransitionManager instance { get; set; }

	[SerializeField] protected TransitionUi _transitionUi;

	public static void Play(TransitionScript script, Action callback = null) => instance.StartCoroutine(instance.DoPlay(script, callback));

	private void Awake() {
		if (instance == null) instance = this;
		if (instance != this) Destroy(gameObject);
		else {
			DontDestroyOnLoad(gameObject);
		}
	}

	private IEnumerator DoPlay(TransitionScript script, Action callback) {
		_transitionUi.SetVisible(true);
		yield return new WaitForSeconds(.5f);
		foreach (var step in script.steps) {
			var stepAudio = AudioManager.Sfx.Play(step.audio);
			_transitionUi.subtitleText = step.subtitles;
			while (stepAudio.isPlaying) yield return null;
			yield return null;
		}
		_transitionUi.SetVisible(false);
		callback?.Invoke();
	}
}