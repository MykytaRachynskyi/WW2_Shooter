using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorPointVisualization : MonoBehaviour
{

	[SerializeField] GameObject prefab;
	[SerializeField] Material material;

	GameObject debugObject;

	void Awake()
	{
		/*debugObject = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
		debugObject.GetComponent<MeshRenderer>().material = material;*/
		int childCount = transform.childCount;
		for (int i = childCount - 1; i >= 0; i--)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}

	void Update()
	{
		//debugObject.transform.position = this.transform.position;
	}

}