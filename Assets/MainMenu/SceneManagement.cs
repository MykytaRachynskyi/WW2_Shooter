using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public string[] levelNames = new string[]
        {
            "Level_1_1",
            "Level_1_2",
        };
    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(levelNames[index]);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void StartCampaign()
    {
        SceneManager.LoadScene("Planning");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}