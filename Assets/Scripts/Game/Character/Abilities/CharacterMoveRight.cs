using UnityEngine;

public class CharacterMoveRight : CharacterAbility {
	protected override void PlayAbility(bool keyDown) {
		if (!keyDown) return;
		transform.position += sheet.speed * Time.deltaTime * Vector3.right;
	}
}