using System;
using UnityEngine;

[CreateAssetMenu(menuName = "TransitionScript")]
public class TransitionScript : ScriptableObject {
	[Serializable]
	public class TransitionStep {
		[SerializeField] protected AudioClip _audio;
		[SerializeField] protected string    _subtitles;

		public string    subtitles => _subtitles;
		public AudioClip audio     => _audio;
	}

	[SerializeField] protected TransitionStep[] _steps;

	public TransitionStep[] steps => _steps;
}