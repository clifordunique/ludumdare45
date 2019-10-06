using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObjectHigher : MonoBehaviour {
	[SerializeField] protected float _minY;

	public void Update() {
		if (transform.position.y >= _minY) return;
		transform.position = new Vector3(transform.position.x, _minY, transform.position.z);
	}
}