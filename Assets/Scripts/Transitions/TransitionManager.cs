using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour {
	public static TransitionManager instance { get; set; }

	[SerializeField] protected TransitionUi     _transitionUi;
	[SerializeField] protected TransitionScript _deadOutro;

	public static void Play(TransitionScript script, Action callback = null) => instance.StartCoroutine(instance.DoPlay(script, callback));

	public static void PlayDeadOutro(Action callback = null) => Play(instance._deadOutro, callback);

	private HashSet<TransitionScript> transitionsToSkip { get; } = new HashSet<TransitionScript>();

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

	public static void SkipInTheFuture(TransitionScript script) => instance.transitionsToSkip.Add(script);
	public static void ClearTransitionsToSkip() => instance.transitionsToSkip.Clear();
	public static bool IsToSkip(TransitionScript script) => instance.transitionsToSkip.Contains(script);
}