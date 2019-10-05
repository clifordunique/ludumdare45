using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class MenuUi : MonoBehaviour {
	private static int hideAnimParam { get; } = Animator.StringToHash("Hide");

	[SerializeField] protected Animator _animator;
	[SerializeField] protected Button   _startButton;

	public UnityEvent onStartClicked => _startButton.onClick;

	public void Hide() {
		_animator.SetTrigger(hideAnimParam);
	}
}