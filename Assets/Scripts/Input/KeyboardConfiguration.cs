using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Keyboard Configuration")]
public class KeyboardConfiguration : ScriptableObject {
	[Serializable]
	public class Mapping {
		[SerializeField] protected KeyCode _gameKey;
		[SerializeField] protected KeyCode _keyboardKey;

		public KeyCode gameKey     => _gameKey;
		public KeyCode keyboardKey => _keyboardKey;
	}

	[SerializeField] protected int       _orderIndex;
	[SerializeField] protected Mapping[] _mappings;

	public int orderIndex => _orderIndex;

	/// <summary>A dictionary mapping the key used in game (same as AZERTY keyboard) and the key on the keyboard of the player</summary>
	private Dictionary<KeyCode, KeyCode> gameToKeyboard { get; set; }

	private Dictionary<KeyCode, KeyCode> keyboardToGame { get; set; }

	public void Init() {
		gameToKeyboard = _mappings.ToDictionary(t => t.gameKey, t => t.keyboardKey);
		keyboardToGame = _mappings.ToDictionary(t => t.keyboardKey, t => t.gameKey);
	}

	public KeyCode GetKeyboardKey(KeyCode gameKey) {
		if (gameToKeyboard == null) Init();
		return gameToKeyboard.ContainsKey(gameKey) ? gameToKeyboard[gameKey] : gameKey;
	}

	public KeyCode GetGameKey(KeyCode keyboardKey) {
		if (keyboardToGame == null) Init();
		return keyboardToGame.ContainsKey(keyboardKey) ? keyboardToGame[keyboardKey] : keyboardKey;
	}
}