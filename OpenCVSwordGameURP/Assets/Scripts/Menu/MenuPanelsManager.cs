using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelsManager : MonoBehaviour
{
    public Animator menuAnimator;

    public bool defaultPanel;
    public bool settingsPanel;
    public bool playPanel;
    public bool cameraSettingsPanel;

    private void Start()
    {
        defaultPanel = true;
        menuAnimator.SetBool("DefaultPanel", true);
    }

    public void DefaultPanel()
    {
        menuAnimator.SetBool("DefaultPanel", true);
        menuAnimator.SetBool("SettingsPanel", false);
        menuAnimator.SetBool("PlayPanel", false);
        menuAnimator.SetBool("CameraSettingsPanel", false);
        defaultPanel = true;
        settingsPanel = false;
        playPanel = false;
        cameraSettingsPanel = false;
    }

    public void SettingsPanel()
    {
        menuAnimator.SetBool("DefaultPanel", false);
        menuAnimator.SetBool("SettingsPanel", true);
        menuAnimator.SetBool("PlayPanel", false);
        menuAnimator.SetBool("CameraSettingsPanel", false);
        defaultPanel = false;
        settingsPanel = true;
        playPanel = false;
        cameraSettingsPanel = false;
    }

    public void PlayPanel()
    {
        menuAnimator.SetBool("DefaultPanel", false);
        menuAnimator.SetBool("SettingsPanel", false);
        menuAnimator.SetBool("PlayPanel", true);
        menuAnimator.SetBool("CameraSettingsPanel", false);
        defaultPanel = false;
        settingsPanel = false;
        playPanel = true;
        cameraSettingsPanel = false;
    }

    public void CameraSettingsPanel()
    {
        menuAnimator.SetBool("DefaultPanel", false);
        menuAnimator.SetBool("SettingsPanel", false);
        menuAnimator.SetBool("PlayPanel", false);
        menuAnimator.SetBool("CameraSettingsPanel", true);
        defaultPanel = false;
        settingsPanel = false;
        playPanel = false;
        cameraSettingsPanel = true;
    }
}
