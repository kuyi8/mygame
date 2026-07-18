using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [Header("按钮")]
    [SerializeField] private Button refreshButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button startMenuButton;

    [Header("设置面板")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button closePanelButton;

    //[Header("刷新确认")]
    //[SerializeField] private GameObject confirmPanel;  // 可选：确认面板
    //[SerializeField] private Button confirmYesButton;
    //[SerializeField] private Button confirmNoButton;

    private AudioSource bgmAudio;

    private float currentVolume = 1f;

    void Start()
    {
        bgmAudio = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();

        // 绑定按钮事件
        refreshButton.onClick.AddListener(OnRefreshClick);
        settingsButton.onClick.AddListener(OnSettingsClick);
        exitGameButton.onClick.AddListener(OnExitGame);
        startMenuButton.onClick.AddListener(OnStartMenu);
        closePanelButton.onClick.AddListener(CloseSettingsPanel);

        // 音量滑动条
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // 加载保存的音量设置
        LoadSettings();

        // 初始关闭设置面板
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        // 确认面板（可选）
        //if (confirmPanel != null)
        //    confirmPanel.SetActive(false);
    }

    // 刷新按钮点击
    void OnRefreshClick()
    {
        // 方式1：直接重启场景（最简单）
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // 方式2：弹出确认面板（防止误触）
        // if (confirmPanel != null)
        //     confirmPanel.SetActive(true);
    }

    // 确认刷新
    void ConfirmRefresh()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 取消刷新
    //void CancelRefresh()
    //{
    //    if (confirmPanel != null)
    //        confirmPanel.SetActive(false);
    //}

    // 设置按钮点击
    void OnSettingsClick()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    // 关闭设置面板
    void CloseSettingsPanel()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    // 音量改变
    void OnVolumeChanged(float value)
    {
        currentVolume = value;
        if (bgmAudio != null)
            bgmAudio.volume = currentVolume;
        PlayerPrefs.SetFloat("GameVolume", currentVolume);
    }

    // 退出游戏
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

    // 加载设置
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
