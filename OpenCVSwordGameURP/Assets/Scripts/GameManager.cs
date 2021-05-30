using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject levelClearedPanel;

    public bool isGameOver;
    public bool levelCleared;

    public bool isGamePaused;

    void Start()
    {
        StartUdp();
        isGameOver = false;
        levelCleared = false;
        isGamePaused = false;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (levelClearedPanel != null) levelClearedPanel.SetActive(false);
    }

    public IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<CannonManager>().ActivateCannons();
    }

    private void StartUdp()
    {
        UdpManager.instance.Connect("127.0.0.1", 8078);

        UdpManager.instance.SendUDPMessage("data");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameOver()
    {
        if (levelCleared) return;

        isGameOver = true;
        gameOverPanel.SetActive(true);
    }

    public IEnumerator PrepareToClearLevel()
    {
        yield return new WaitForSeconds(10f);
        levelCleared = !isGameOver;

        if (levelCleared) LevelCleared();
    }

    private void LevelCleared()
    {
        levelClearedPanel.SetActive(true);
    }

    public void LoadLevel0()
    {
        if (SceneManager.GetSceneByName("Level 0") != null)
            SceneManager.LoadScene("Level 0");
    }

    public void LoadLevel1()
    {
        if(SceneManager.GetSceneByName("Level 1") != null)
            SceneManager.LoadScene("Level 1");
    }

    public void LoadLevel2()
    {
        if (SceneManager.GetSceneByName("Level 2") != null)
            SceneManager.LoadScene("Level 2");
    }

    public void LoadLevel3()
    {
        if (SceneManager.GetSceneByName("Level 3") != null)
            SceneManager.LoadScene("Level 3");
    }

    public void LoadLevel4()
    {
        if (SceneManager.GetSceneByName("Level 4") != null)
            SceneManager.LoadScene("Level 4");
    }

    public void LoadLevel5()
    {
        if (SceneManager.GetSceneByName("Level 5") != null)
            SceneManager.LoadScene("Level 5");
    }

    public void LoadLevel6()
    {
        if (SceneManager.GetSceneByName("Level 6") != null)
            SceneManager.LoadScene("Level 6");
    }

    public void LoadLevel7()
    {
        if (SceneManager.GetSceneByName("Level 7") != null)
            SceneManager.LoadScene("Level 7");
    }

    public void LoadMainMenu()
    {
        if (SceneManager.GetSceneByName("Main Menu") != null)
        {
            FindObjectOfType<PauseManager>().isGamePaused = false;
            FindObjectOfType<GameManager>().isGamePaused = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene("Main Menu");
        }
    }
}
