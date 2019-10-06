using UnityEngine;

public static class Vector3Extension {
	public static Vector3 With(this Vector3 v3, float? x = null, float? y = null, float? z = null) => new Vector3(x ?? v3.x, y ?? v3.y, z ?? v3.z);
}