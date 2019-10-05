using UnityEngine;

public class CharacterSheet : MonoBehaviour {
	[SerializeField] protected float _speed;
	[SerializeField] protected float _jumpForce;

	public float speed     => _speed;
	public float jumpForce => _jumpForce;
}