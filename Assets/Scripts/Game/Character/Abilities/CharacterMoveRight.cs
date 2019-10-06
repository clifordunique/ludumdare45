using UnityEngine;

public class CharacterMoveRight : CharacterAbility {
	protected override void PlayAbility(bool keyDown) {
		animator.walkingRight = keyDown;
		if (!keyDown) return;
		animator.faceLeft = false;
		transform.position += sheet.speed * Time.deltaTime * Vector3.right;
	}
}