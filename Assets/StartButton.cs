using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{

	[SerializeField] ObjectPlacement objectPlacement;
	void Update()
	{
		if (!this.GetComponent<Button>().interactable &&
			objectPlacement.allies[0].avaliableCount == 0 &&
			objectPlacement.allies[1].avaliableCount == 0 &&
			objectPlacement.allies[2].avaliableCount == 0 &&
			objectPlacement.availableBombs == 0)
			this.GetComponent<Button>().interactable = true;
	}
}