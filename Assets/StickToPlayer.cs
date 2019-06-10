using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPlayer : MonoBehaviour
{

	[SerializeField] Transform tile;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		this.transform.position = Camera.main.WorldToScreenPoint(tile.position);
	}
}