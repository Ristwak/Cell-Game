using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public GameObject table;
    public GameObject cell;
    public Canvas gameCanvas;
    public Canvas homeUi;
    public GameObject loadingScreen;
    public GameObject gameOverPanel;

    private void Start()
    {
        homeUi.enabled = false;
        gameCanvas.enabled = true;
    }

    public void DeactivateGameObjects()
    {
        if (table != null)
        {
            table.SetActive(false);
        }
        if (cell != null)
        {
            cell.SetActive(false);
        }
        if (gameCanvas != null)
        {
            gameCanvas.enabled = false;
        }
        if (homeUi != null)
        {
            homeUi.enabled = true;
            gameOverPanel.SetActive(true);
            loadingScreen.SetActive(false);
        }
    }

    public void Retry()
    {
        gameOverPanel.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("HomeScene");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}
