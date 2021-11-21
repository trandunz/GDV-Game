using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_ButtonEvents : MonoBehaviour
{
    [SerializeField] Slider m_Audio;
    [SerializeField] Button m_Apply;

    public void StartNewGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Fullscreen()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 0)
        {
            Screen.SetResolution(1280, 720, true);
        }
        else if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 1)
        {
            Screen.SetResolution(1366, 768, true);
        }
        else if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 2)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 3)
        {
            Screen.SetResolution(2560, 1440, true);
        }
    }

    public void Windowed()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);
        if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 0)
        {
            Screen.SetResolution(1280, 720, false);
        }
        else if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 1)
        {
            Screen.SetResolution(1366, 768, false);
        }
        else if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 2)
        {
            Screen.SetResolution(1920, 1080, false);
        }
        else if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 3)
        {
            Screen.SetResolution(2560, 1440, false);
        }
    }

    public void QuitToMainMenu()
    {
        if (!GameObject.FindGameObjectWithTag("CrewMate"))
        {
            ExitGame();
        }
        else
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }

    }

    public void SetAudio()
    {
        AudioListener.volume = m_Audio.value;
    }

    public void ApplyChanges()
    {
        if (Screen.fullScreen == true)
        {
            if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 0)
            {
                Screen.SetResolution(1280, 720, true);
            }
            else if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 1)
            {
                Screen.SetResolution(1366, 768, true);
            }
            else if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 2)
            {
                Screen.SetResolution(1920, 1080, true);
            }
            else if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 3)
            {
                Screen.SetResolution(2560, 1440, true);
            }
        }
        else
        {
            if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 0)
            {
                Screen.SetResolution(1280, 720, false);
            }
            else if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 1)
            {
                Screen.SetResolution(1366, 768, false);
            }
            else if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 2)
            {
                Screen.SetResolution(1920, 1080, false);
            }
            else if (GetComponentInChildren<TMPro.TMP_Dropdown>().value == 3)
            {
                Screen.SetResolution(2560, 1440, false);
            }
        }

        SetInteractable(m_Apply, false);
    }

    public void Mute()
    {
        AudioListener.volume = 0.0f;
    }

    public void UnMute()
    {
        AudioListener.volume = 1.0f;
    }

    void ExitGame()
    {
        Application.Quit();
    }

    public void SetInteractable(Button _object, bool _truth)
    {
        _object.interactable = _truth;
    }
}
