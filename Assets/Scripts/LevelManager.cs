using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [HideInInspector]public List<Scene> SceneList;
    [HideInInspector]public static LevelManager instance;

    [HideInInspector] private GameObject _loaderCanvas;
    [HideInInspector] private Image _progressBar;

    [HideInInspector]public float _target;

    private IEnumerator loadCor;


    // Start jest wywoływany przed aktualizacją pierwszej ramki
    void Awake()
    {
        if(instance==null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public async void LoadScene(string SceneName)
    {
        _target =0;
        var scene= SceneManager.LoadSceneAsync(SceneName);
        //Nie przechodź automatycznie do następnej sceny
        scene.allowSceneActivation = false;

        //_loaderCanvas.SetActive(true);
        //Load without screen
        scene.allowSceneActivation = true;

        //StartCoroutine(loadBar (1.2f,scene,SceneName));        

    }

    public void LoadScene(string sceneName, float duration)
    {
        _target = 0;

        // Start loading the scene asynchronously
        var sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName);
        sceneLoadOperation.allowSceneActivation = false; // Prevent automatic activation

        // Show the loading UI
        //_loaderCanvas.SetActive(true);

        //Load without screen
        sceneLoadOperation.allowSceneActivation = true;

        // Start the load bar coroutine
        //StartCoroutine(loadBar(duration, sceneLoadOperation, sceneName));
    }


    IEnumerator loadBar(float duration, AsyncOperation scene, string sceneName)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration || scene.progress < 0.9f)
        {
            // Update the progress bar based on the elapsed time and duration
            float progress = Mathf.Clamp01(elapsedTime / duration);
            _progressBar.fillAmount = Mathf.Clamp01(Mathf.Min(progress, scene.progress / 0.9f));

            // Increment time
            elapsedTime += Time.deltaTime;

            // Yield to wait for the next frame
            yield return null;
        }

        // Once both the timer and scene progress are complete
        _progressBar.fillAmount = 1;

        // Allow the scene to activate
        scene.allowSceneActivation = true;

        // Wait briefly before hiding the loader canvas
        yield return new WaitForSeconds(0.5f);
        _loaderCanvas.SetActive(false);

        // Handle specific scene actions
        if (sceneName == "MainMenu")
        {
            MainMenu_Handler.instance.Activate_MainMenu();
        }
    }


}
