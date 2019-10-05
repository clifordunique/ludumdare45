using System.Collections.Generic;
using UnityEngine;

public class PositionChecker : MonoBehaviour {
	[SerializeField] protected string[] _layers;

	public bool isValid => items.Count > 0;

	private HashSet<Collider2D> items { get; } = new HashSet<Collider2D>();

	public void OnTriggerEnter2D(Collider2D collision) {
		if (1 << collision.gameObject.layer == LayerMask.GetMask(_layers)) {
			items.Add(collision);
		}
	}

	public void OnTriggerExit2D(Collider2D collision) => items.Remove(collision);
}