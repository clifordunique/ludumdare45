using UnityEngine;

public class SidePositionChecker : MonoBehaviour {
	[SerializeField] protected PositionChecker _leftChecker;
	[SerializeField] protected PositionChecker _rightChecker;

	public bool isValid      => isLeftValid || isRightValid;
	public bool isLeftValid  => _leftChecker.isValid;
	public bool isRightValid => _rightChecker.isValid;
}