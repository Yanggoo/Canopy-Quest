using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    public GameObject seedInput;
    // Start is called before the first frame update

    public void SetSeedTrigger(bool ison)
    {
        seedInput.SetActive(ison);
        seedInput.GetComponent<InputField>().text = "";
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(AsyncLoadLevel(sceneIndex));
    }

    IEnumerator AsyncLoadLevel(int sceneIndex)
    {
        var inputSeedCode = seedInput.GetComponent<InputField>().text;
        if (inputSeedCode != "" && int.TryParse(inputSeedCode,out int code))
        {
            DataHolder.instance.seedCode = code;
        }
        else
        {
            DataHolder.instance.seedCode = null;
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            slider.value = progress;
            progressText.text = Mathf.FloorToInt(progress * 100).ToString() + "%";
            yield return null;
        }
        TreeDungeonGenerator.instance.GenerateDungeon();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
