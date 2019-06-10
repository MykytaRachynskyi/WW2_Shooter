using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategySettings : MonoBehaviour {

	struct PlacementData {
		public TileContent tileContent;
		public Vector3 position;
		public PlacementData (TileContent tileContent, Vector3 position) {
			this.tileContent = tileContent;
			this.position = position;
		}
	}

	[SerializeField] GameObject allyPrefab;
	[SerializeField] GameObject bombPrefab;
	[SerializeField] LayerMask layerMask;
	List<PlacementData> objectPlacementData;
	sGridTile[] gridTiles;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (this.gameObject);
	}

	private void Start () {
		gridTiles = GameObject.FindObjectsOfType<sGridTile> ();
		Debug.Log ("Found " + gridTiles.Length + " tiles.");
	}

	public void GatherObjectPlacementData () {
		objectPlacementData = new List<PlacementData> ();
		foreach (sGridTile tile in gridTiles) {
			if (tile.tileContent != TileContent.None) {
				objectPlacementData.Add (new PlacementData (tile.tileContent, tile.transform.position));
			}
		}
	}

	public void PlaceObjects () {
		Debug.Log ("Placing " + objectPlacementData.Count);
		Ray ray;
		RaycastHit hit;
		foreach (PlacementData item in objectPlacementData) {
			if (item.tileContent == TileContent.Bomb) {
				Debug.Log ("Placing bomb");
				ray = new Ray (item.position, Vector3.down);
				if (Physics.Raycast (ray, out hit, 100f, layerMask)) {
					Debug.Log ("Placing bomb success");
					Instantiate (bombPrefab, hit.point, Quaternion.identity);
				}
			} else if (item.tileContent == TileContent.Ally) {
				Debug.Log ("Placing ally");
				ray = new Ray (item.position, Vector3.down);
				if (Physics.Raycast (ray, out hit, 100f, layerMask)) {
					Debug.Log ("Placing ally success");
					Instantiate (allyPrefab, hit.point, Quaternion.identity);
				}
			}
		}
	}
}