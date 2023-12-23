using UnityEngine;

public class DataHolder : MonoBehaviour
{
    // ������Է�����Ҫ���������
    [SerializeField]
    public int? seedCode;

    // ʹ�õ���ģʽȷ��ֻ��һ��ʵ��
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
