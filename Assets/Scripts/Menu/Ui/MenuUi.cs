using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class MenuUi : MonoBehaviour {
	private static int hideAnimParam { get; } = Animator.StringToHash("Hide");

	[SerializeField] protected Animator _animator;
	[SerializeField] protected Button   _startButton;

	[Header("Configuration")] [SerializeField] protected KeyboardLayoutButton[] _keyboardLayoutOptions;
	[SerializeField]                           protected Slider                 _musicVolume;
	[SerializeField]                           protected Slider                 _sfxVolume;
	[SerializeField]                           protected Slider                 _voiceVolume;

	public UnityEvent<KeyboardConfiguration> onKeyboardLayoutChange { get; } = new KeyboardLayoutButton.KeyboardLayoutButtonClicked();
	public UnityEvent                        onStartClicked         => _startButton.onClick;
	public UnityEvent<float>                 onMusicVolumeChanged   => _musicVolume.onValueChanged;
	public UnityEvent<float>                 onSfxVolumeChanged     => _sfxVolume.onValueChanged;
	public UnityEvent<float>                 onVoiceVolumeChanged   => _voiceVolume.onValueChanged;

	private void Awake() {
		foreach (var keyboardLayoutOption in _keyboardLayoutOptions) keyboardLayoutOption.onClick.AddListener(onKeyboardLayoutChange.Invoke);
	}

	public void SetActiveKeyboardLayout(KeyboardConfiguration layout) {
		foreach (var option in _keyboardLayoutOptions) option.active = option.layout == layout;
	}

	public void Hide() {
		_animator.SetTrigger(hideAnimParam);
	}

	public void SetAudioVolumes(float music, float sfx, float voice) {
		_musicVolume.value = music;
		_sfxVolume.value = sfx;
		_voiceVolume.value = voice;
	}
}