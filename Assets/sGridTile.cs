using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sGridTile : MonoBehaviour {

	public TileContent tileContent = TileContent.None;
}

public enum TileContent { None, Ally, Bomb}