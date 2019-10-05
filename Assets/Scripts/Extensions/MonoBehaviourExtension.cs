using UnityEngine;

public static class MonoBehaviourExtension {
	public static bool TryGetComponent<E>(this MonoBehaviour mb, out E e) where E : Component {
		e = mb.GetComponent<E>();
		return e;
	}
}