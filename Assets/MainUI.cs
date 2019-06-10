using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
	public CanvasGroup[] menus;

	public void MainMenu()
	{
		menus[0].alpha = 1;
		menus[1].alpha = 0;
	}

	public void CampaignSelection()
	{
		menus[0].alpha = 0;
		menus[1].alpha = 1;
	}
}