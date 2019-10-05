using System;
using UnityEngine;

public class CameraManager : MonoBehaviour {
	public enum Behaviour {
		Follow,
		GlobalView
	}

	[SerializeField] protected Camera        _mainCamera;
	[SerializeField] protected FollowCam     _followCam;
	[SerializeField] protected GlobalViewCam _globalViewCam;
	[SerializeField] protected Behaviour     _behaviour;
	[SerializeField] protected float         _changeBehaviourSpeed = 1f;

	private float timeOnChangeBehaviour  { get; set; }
	private float targetOrthographicSize => GetBehaviour().orthographicSize;

	private CameraBehaviour GetBehaviour() {
		switch (behaviour) {
			case Behaviour.Follow: return _followCam;
			case Behaviour.GlobalView: return _globalViewCam;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	public Behaviour behaviour {
		get => _behaviour;
		set {
			if (_behaviour == value) return;
			_behaviour = value;
			_mainCamera.transform.SetParent(GetBehaviour().transform);
			timeOnChangeBehaviour = Time.time;
		}
	}

	public void Update() {
		UpdatePosition();
		UpdateOrthographicSize();
	}

	private void UpdatePosition() {
		if (_mainCamera.transform.localPosition.x == 0 && _mainCamera.transform.localPosition.y == 0) return;
		_mainCamera.transform.localPosition = Vector3.Lerp(_mainCamera.transform.localPosition, new Vector3(0, 0, -10), (Time.time - timeOnChangeBehaviour) / _changeBehaviourSpeed);
	}

	private void UpdateOrthographicSize() {
		if (_mainCamera.orthographicSize == targetOrthographicSize) return;
		_mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, targetOrthographicSize, (Time.time - timeOnChangeBehaviour) / _changeBehaviourSpeed);
	}
}