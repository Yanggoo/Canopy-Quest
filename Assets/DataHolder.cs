using UnityEngine;

public class DataHolder : MonoBehaviour
{
    // 这里可以放置需要保存的数据
    [SerializeField]
    public int? seedCode;

    // 使用单例模式确保只有一个实例
    public static DataHolder instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
