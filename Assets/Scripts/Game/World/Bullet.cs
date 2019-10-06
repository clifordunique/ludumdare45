using UnityEngine;

namespace Game.World {
	public class Bullet : MonoBehaviour {
		[SerializeField] protected float   _speed          = 5;
		[SerializeField] protected float   _lifeSpan       = 5;
		[SerializeField] protected Vector3 _movementVector = Vector3.right;

		private float birthTime { get; set; }

		public Vector3 movementVector {
			get => _movementVector;
			set => _movementVector = value.normalized;
		}

		private void Awake() {
			birthTime = Time.time;
		}

		private void Update() {
			if (birthTime + _lifeSpan < Time.time) Destroy(gameObject);
			transform.position += Time.deltaTime * _speed * _movementVector;
		}

		public void OnTriggerEnter2D(Collider2D other) {
			if (other.gameObject.layer != LayerMask.NameToLayer("UnlockAbility")) Destroy(gameObject);
		}
	}
}