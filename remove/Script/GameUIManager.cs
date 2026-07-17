using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private Button refreshButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button startMenuButton;

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button closePanelButton;

    private AudioSource bgmAudio;
    private float currentVolume = 1f;

    void Start()
    {
        bgmAudio = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();

        refreshButton.onClick.AddListener(OnRefreshClick);
        settingsButton.onClick.AddListener(OnSettingsClick);
        exitGameButton.onClick.AddListener(OnExitGame);
        startMenuButton.onClick.AddListener(OnStartMenu);
        closePanelButton.onClick.AddListener(CloseSettingsPanel);

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        LoadSettings();

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    void OnRefreshClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ConfirmRefresh()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnSettingsClick()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    void CloseSettingsPanel()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    void OnVolumeChanged(float value)
    {
        currentVolume = value;
        if (bgmAudio != null)
            bgmAudio.volume = currentVolume;
        PlayerPrefs.SetFloat("GameVolume", currentVolume);
    }

    void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OnStartMenu()
    {
        SceneManager.LoadScene("startMenu");
    }

    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("GameVolume"))
        {
            currentVolume = PlayerPrefs.GetFloat("GameVolume");
            volumeSlider.value = currentVolume;
            AudioListener.volume = currentVolume;
        }
    }
}
