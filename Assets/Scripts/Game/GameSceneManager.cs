using UnityEngine;

public class GameSceneManager : MonoBehaviour {
	[SerializeField] protected CageBreaker _cageBreaker;

	private void Awake() {
		_cageBreaker.Break();
	}
}