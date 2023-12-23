using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    void Start()
    {
        // ע�᳡��������ɺ���¼�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // �ڶ�������ʱȡ���¼�ע�ᣬ�Է�ֹǱ�ڵ��ڴ�й©
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SwitchToNextScene()
    {
        // �л�����һ������
        SceneManager.LoadScene("NextScene");
    }

    // �ڳ���������ɺ���õĺ���
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ��������Ե�������Ҫִ�еĺ���
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
