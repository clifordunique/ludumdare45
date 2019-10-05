using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour {
	private static App instance { get; set; }

	public static KeyboardConfiguration keyboardConfiguration {
		get => instance._keyboardConfiguration;
		set => instance._keyboardConfiguration = value;
	}

	[SerializeField] private KeyboardConfiguration _keyboardConfiguration;

	private void Awake() {
		if (instance == null) instance = this;
		if (instance != this) Destroy(gameObject);
		else {
			DontDestroyOnLoad(gameObject);
		}
	}

}