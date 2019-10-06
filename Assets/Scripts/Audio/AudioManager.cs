using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	private static AudioManager instance { get; set; }

	[SerializeField]               protected AudioSource _audioSourcePrefab;
	[SerializeField]               protected AudioSource _musicSource;
	[SerializeField] [Range(0, 1)] protected float       _musicVolume;
	[SerializeField] [Range(0, 1)] protected float       _sfxVolume;
	[SerializeField] [Range(0, 1)] protected float       _voiceVolume;

	private HashSet<AudioSource> sfxSources { get; } = new HashSet<AudioSource>();

	private AudioSource GetAudioSource() {
		var source = instance.sfxSources.FirstOrDefault(t => !t.isPlaying);
		if (source) return source;
		source = Instantiate(instance._audioSourcePrefab, instance.transform);
		instance.sfxSources.Add(source);
		return source;
	}

	private void Reset() {
		if (!_musicSource) _musicSource = GetComponent<AudioSource>();
	}

	private void Awake() {
		if (instance == null) instance = this;
		if (instance != this) Destroy(gameObject);
		else {
			DontDestroyOnLoad(gameObject);
			_musicSource.volume = _musicVolume;
		}
	}

	public static class Music {
		public static float volume {
			get => instance._musicVolume;
			set {
				instance._musicVolume = value;
				instance._musicSource.volume = value;
			}
		}
	}

	public static class Sfx {
		public static float volume {
			get => instance._sfxVolume;
			set => instance._sfxVolume = value;
		}

		public static AudioSource Play(string clipName) => Play(Resources.Load<AudioClip>($"Audio/Sfx/{clipName}"));

		public static AudioSource Play(AudioClip clip) {
			var source = instance.GetAudioSource();
			source.volume = volume;
			source.clip = clip;
			source.Stop();
			source.Play();
			return source;
		}
	}

	public static class Voice {
		public static float volume {
			get => instance._voiceVolume;
			set => instance._voiceVolume = value;
		}

		public static AudioSource Play(string clipName) => Play(Resources.Load<AudioClip>($"Audio/Voice/{clipName}"));

		public static AudioSource Play(AudioClip clip) {
			var source = instance.GetAudioSource();
			source.volume = volume;
			source.clip = clip;
			source.Stop();
			source.Play();
			return source;
		}
	}
}