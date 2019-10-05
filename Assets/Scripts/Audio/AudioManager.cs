using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	private static AudioManager instance { get; set; }

	[Header("Music")] [SerializeField] protected AudioSource _musicSource;
	[SerializeField] [Range(0, 1)]     protected float       _musicVolume;
	[Header("Sfx")] [SerializeField]   protected AudioSource _sfxPrefab;
	[SerializeField] [Range(0, 1)]     protected float       _sfxVolume;

	private HashSet<AudioSource> sfxSources { get; } = new HashSet<AudioSource>();

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
			set {
				instance._sfxVolume = value;
				foreach (var existingSource in instance.sfxSources) existingSource.volume = value;
			}
		}

		public static AudioSource Play(string clipName) => Play(Resources.Load<AudioClip>($"Audio/Sfx/{clipName}"));

		public static AudioSource Play(AudioClip clip) {
			var source = instance.sfxSources.FirstOrDefault(t => !t.isPlaying);
			if (!source) {
				source = Instantiate(instance._sfxPrefab, instance.transform);
				instance.sfxSources.Add(source);
				source.volume = volume;
			}
			source.clip = clip;
			source.Stop();
			source.Play();
			return source;
		}
	}
}