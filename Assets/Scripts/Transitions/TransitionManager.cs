using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour {
	public static TransitionManager instance { get; set; }

	[SerializeField] protected TransitionUi     _transitionUi;
	[SerializeField] protected TransitionScript _deadOutro;

	private Coroutine coroutine { get; set; }

	public static void Play(TransitionScript script, Action callback = null, float delayBeforeCallbackOnNoPlay = 0f) {
		if (!script || instance.transitionsToSkip.Contains(script)) {
			if (callback != null) instance.StartCoroutine(WaitAndCall(callback, delayBeforeCallbackOnNoPlay));
		}
		else {
			if (instance.coroutine != null) instance.StopCoroutine(instance.coroutine);
			instance.coroutine = instance.StartCoroutine(instance.DoPlay(script, callback));
			instance.transitionsToSkip.Add(script);
		}
	}

	private static IEnumerator WaitAndCall(Action callback, float delay) {
		yield return new WaitForSeconds(delay);
		callback.Invoke();
	}

	public static void PlayDeadOutro(Action callback = null) => Play(instance._deadOutro, callback, .5f);

	private HashSet<TransitionScript> transitionsToSkip { get; } = new HashSet<TransitionScript>();

	private void Awake() {
		if (instance == null) instance = this;
		if (instance != this) Destroy(gameObject);
		else {
			DontDestroyOnLoad(gameObject);
		}
	}

	private IEnumerator DoPlay(TransitionScript script, Action callback) {
		_transitionUi.subtitleText = string.Empty;
		_transitionUi.SetVisible(true);
		var musicVolume = AudioManager.Music.volume;
		while (AudioManager.Music.volume > musicVolume / 4) {
			AudioManager.Music.volume -= Time.deltaTime * musicVolume;
			yield return null;
		}
		foreach (var step in script.steps) {
			var stepAudio = AudioManager.Voice.Play(step.audio);
			_transitionUi.subtitleText = step.subtitles;
			while (stepAudio.isPlaying) yield return null;
			yield return new WaitForSeconds(.2f);
		}
		_transitionUi.SetVisible(false);
		while (AudioManager.Music.volume < musicVolume) {
			AudioManager.Music.volume += 3 * Time.deltaTime * musicVolume;
			yield return null;
		}
		AudioManager.Music.volume = musicVolume;
		callback?.Invoke();
	}

	public static void ClearTransitionsToSkip() => instance.transitionsToSkip.Clear();
}