using System.Collections.Generic;
using UnityEngine;

public class UnlockedNotificationUi : MonoBehaviour {
	private static string notificationPattern { get; } = "You can now press <b>{0}</b> to <b>{1}</b>.";

	private static IReadOnlyDictionary<KeyCode, string> actionsPerKey { get; } = new Dictionary<KeyCode, string> {
		{KeyCode.D, "Move right"},
		{KeyCode.Q, "Move left"},
		{KeyCode.P, "See the whole level"},
		{KeyCode.Space, "Jump"},
		{KeyCode.Z, "Climb walls"},
		{KeyCode.X, "Fly"},
		{KeyCode.F, "Shoot seeds"}
	};

	[SerializeField] protected TMPro.TMP_Text _text;

	public void ShowText(KeyCode keyCode) {
		_text.text = string.Format(notificationPattern, App.keyboardConfiguration.GetKeyboardKey(keyCode), actionsPerKey.ContainsKey(keyCode) ? actionsPerKey[keyCode] : "Something");
	}
}