using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsAvailableUI : MonoBehaviour
{

	[SerializeField] ObjectPlacement objectPlacement;
	[SerializeField] Text availableSnipersText;
	[SerializeField] Text availableGunnersText;
	[SerializeField] Text availableRiflemenText;
	[SerializeField] Text bombsAvailableText;

	void Update()
	{
		availableSnipersText.text = objectPlacement.allies[0].avaliableCount.ToString();
		availableGunnersText.text = objectPlacement.allies[1].avaliableCount.ToString();
		availableRiflemenText.text = objectPlacement.allies[2].avaliableCount.ToString();

		bombsAvailableText.text = objectPlacement.availableBombs.ToString();
	}
}