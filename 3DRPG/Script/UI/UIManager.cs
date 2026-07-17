using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider Progress;
    public Image BgImage;
    public Image LoadImage;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(OnStartGame);
        exitButton.onClick.AddListener(OnExitGame);
    }

    void OnStartGame()
    {
        //SceneManager.LoadScene("Environment_Free");
        BgImage.gameObject.SetActive(false);
        LoadImage.gameObject.SetActive(false);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone)
        { 
            Progress.value = operation.progress;
            yield return null;
        }
    }


    void OnExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
