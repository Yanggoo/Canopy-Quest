using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    void Start()
    {
        // 注册场景加载完成后的事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // 在对象被销毁时取消事件注册，以防止潜在的内存泄漏
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SwitchToNextScene()
    {
        // 切换到下一个场景
        SceneManager.LoadScene("NextScene");
    }

    // 在场景加载完成后调用的函数
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 在这里可以调用你想要执行的函数
        if (scene.name == "Tree")
        {
            DoSomethingOnNextSceneLoad();
        }
    }

    void DoSomethingOnNextSceneLoad()
    {
        //TreeDungeonGenerator.instance.CreateDungeon();
    }
}
