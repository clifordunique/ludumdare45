using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
	[SerializeField] protected float _delayToRedirect;

	public void Awake() {
		StartCoroutine(Redirect());
	}

	private IEnumerator Redirect() {
		yield return new WaitForSeconds(_delayToRedirect);
		SceneManager.LoadSceneAsync("Menu");
	}
}