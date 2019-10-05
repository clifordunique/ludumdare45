using UnityEngine;

public class CharacterGlobalView : CharacterAbility {
	[SerializeField] protected CameraManager _cameraManager;

	protected override void PlayAbility(bool keyDown) {
		_cameraManager.behaviour = keyDown ? CameraManager.Behaviour.GlobalView : CameraManager.Behaviour.Follow;
	}
}