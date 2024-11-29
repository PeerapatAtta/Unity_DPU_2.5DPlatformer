using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelectPanel, mainMenuPanel;
    public string mainMenuScene;
    public string[] levelNames;

    public void OpenLevelSelect()
    {
        levelSelectPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void CloseLevelSelect()
    {
        levelSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(levelNames[0]);
    }

    public void Level1()
    {
        SceneManager.LoadScene(levelNames[0]);
    }

    public void Level2()
    {
        SceneManager.LoadScene(levelNames[1]);
    }

    public void Level3()
    {
        SceneManager.LoadScene(levelNames[2]);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}


