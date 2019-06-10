using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyParentMethodCaller : MonoBehaviour
{

	AICharacterControl control;
	// Use this for initialization
	void Awake()
	{
		control = this.transform.parent.GetComponent<AICharacterControl>();
	}

	public void Shoot()
	{
		control.Shoot();
	}
}