using UnityEngine;

public class CharacterMoveLeft : CharacterAbility {
	protected override void PlayAbility(bool keyDown) {
		animator.walkingLeft = keyDown;
		if (!keyDown) return;
		animator.faceLeft = true;
		transform.position += sheet.speed * Time.deltaTime * Vector3.left;
	}
}