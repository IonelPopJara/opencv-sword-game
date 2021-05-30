using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public GameManager gameManager;
    public float currentTimeScale;
    public bool slowMotion;
    public AudioMixerSnapshot defaultSnapshot;
    public AudioMixerSnapshot lowPassSnapshot;

    private void Start()
    {
        slowMotion = false;
    }

    private void Update()
    {
        currentTimeScale = Time.timeScale;

        if (currentTimeScale < 1 && !slowMotion)
        {
            slowMotion = true;
            lowPassSnapshot.TransitionTo(.01f);
        }
        if (currentTimeScale >= 1 && slowMotion)
        {
            slowMotion = false;
            defaultSnapshot.TransitionTo(.01f);
        }
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        Lowspass();
    }

    public void Lowspass()
    {
        if (Time.timeScale == 0)
        {
            lowPassSnapshot.TransitionTo(.01f);
        }
        else
        {
            defaultSnapshot.TransitionTo(.01f);
        }
    }
}
