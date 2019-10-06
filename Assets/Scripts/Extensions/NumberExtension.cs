public static class NumberExtension {
	public static float RoundDown(this float f) {
		if (f >= 0 || f == (int) f) return (int) f;
		return (int) f - 1;
	}
}