using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuUi : MonoBehaviour {
	[SerializeField] protected Button _startButton;

	public UnityEvent onStartClicked => _startButton.onClick;
}