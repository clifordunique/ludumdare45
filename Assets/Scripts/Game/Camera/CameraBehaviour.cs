using UnityEngine;

public abstract class CameraBehaviour : MonoBehaviour {
	[SerializeField] protected float _orthographicSize;

	public float orthographicSize => _orthographicSize;
}