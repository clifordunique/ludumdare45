using UnityEngine;

public class FollowCam : MonoBehaviour {
	[SerializeField] protected Transform _target;
	[SerializeField] protected float     _lerpSpeed;

	private void FixedUpdate() {
		if (!_target) return;
		transform.position = Vector2.Lerp(transform.position, _target.position, _lerpSpeed * Time.deltaTime);
	}
}