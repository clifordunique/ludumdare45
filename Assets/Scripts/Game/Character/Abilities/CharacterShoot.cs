using Game.World;
using UnityEngine;

public class CharacterShoot : CharacterAbility {
	[SerializeField] protected Bullet    _bulletPrefab;
	[SerializeField] protected float     _cooldown = 1;
	[SerializeField] protected Transform _spawnPositionLeft;
	[SerializeField] protected Transform _spawnPositionRight;
	[SerializeField] protected Transform _spawnPositionUp;
	[SerializeField] protected AudioClip _shootAudioClip;

	private float _lastShootTime { get; set; } = -100;

	protected override void PlayAbility(bool keyDown) {
		if (!keyDown) return;
		if (_lastShootTime + _cooldown > Time.time) return;
		if (animator.gripped) Instantiate(_bulletPrefab, _spawnPositionUp.position, Quaternion.identity, null).movementVector = Vector3.up;
		else if (animator.faceLeft) Instantiate(_bulletPrefab, _spawnPositionLeft.position, Quaternion.identity, null).movementVector = Vector3.left;
		else Instantiate(_bulletPrefab, _spawnPositionRight.position, Quaternion.identity, null).movementVector = Vector3.right;
		_lastShootTime = Time.time;
		animator.SetShoot();
		AudioManager.Sfx.Play(_shootAudioClip);
	}
}