using UnityEngine;

public class ScriptedTransition : MonoBehaviour {
	[SerializeField] protected TransitionScript _transitionScript;

	private bool played { get; set; }

	public void OnTriggerEnter2D(Collider2D other) {
		if (played) return;
		TransitionManager.Play(_transitionScript);
		played = true;
		enabled = false;
	}
}