using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void Exit () {
		SceneManager.LoadScene ("StrategyTest");
	}

	public void Quit () {
		Application.Quit ();
	}
}