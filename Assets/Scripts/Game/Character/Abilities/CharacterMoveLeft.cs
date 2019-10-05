using UnityEngine;

public class CharacterMoveLeft : CharacterAbility {
	protected override void PlayAbility(bool keyDown) {
		if (!keyDown) return;
		transform.position += sheet.speed * Time.deltaTime * Vector3.left;
	}
}