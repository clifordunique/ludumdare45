using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CageBreaker : MonoBehaviour {
	[SerializeField] protected Tilemap     _groundTileMap;
	[SerializeField] protected Tilemap     _foregroundTileMap;
	[SerializeField] protected Vector3Int  _breakTilePosition;
	[SerializeField] protected Tile[]      _groundSteps;
	[SerializeField] protected Tile[]      _foregroundSteps;
	[SerializeField] protected float[]     _stepTimers;
	[SerializeField] protected AudioClip[] _stepAudios;

	public void Break() => StartCoroutine(DoBreak());

	private IEnumerator DoBreak() {
		for (var nextIndex = 0; nextIndex < _groundSteps.Length + _foregroundSteps.Length; nextIndex++) {
			var delayBeforeNextStep = _stepTimers[nextIndex];
			while (delayBeforeNextStep > 0) {
				yield return null;
				delayBeforeNextStep -= Time.deltaTime;
			}
			_groundTileMap.SetTile(_breakTilePosition, _groundSteps.Length > nextIndex ? _groundSteps[nextIndex] : null);
			_foregroundTileMap.SetTile(_breakTilePosition, _groundSteps.Length > nextIndex ? null : _foregroundSteps[nextIndex - _groundSteps.Length]);
			if (_stepAudios[nextIndex]) AudioManager.Sfx.Play(_stepAudios[nextIndex]);
		}
	}
}