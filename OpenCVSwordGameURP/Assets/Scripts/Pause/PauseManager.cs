using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    public bool isGamePaused;
    public float currentTimeScale;
    public float previousTimeScale;

    private void Start()
    {
        isGamePaused = false;

        pausePanel.SetActive(false);
    }

    void Update()
    {
        currentTimeScale = Time.timeScale;

        FindObjectOfType<GameManager>().isGamePaused = isGamePaused;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            Pause();
        }
    }

    private void Pause()
    {
        previousTimeScale = Time.timeScale;

        if (isGamePaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0.0000000000f;
        }
        else if (!isGamePaused)
        {
            pausePanel.SetActive(false);
            Time.timeScale = previousTimeScale;
        }
    }
}
