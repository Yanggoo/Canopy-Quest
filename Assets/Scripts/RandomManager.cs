using System;

public class RandomManager
{
    private static RandomManager instance;
    private Random random;

    private RandomManager(int seed)
    {
        random = new Random(seed);
    }

    public static RandomManager Instance
    {
        get
        {
            if (instance == null)
            {
                // �ڵ�һ�η���ʱ��ʹ��Ĭ�����Ӵ���ʵ��
                instance = new RandomManager(Environment.TickCount);
            }
            return instance;
        }
    }

    // �������������
    public void SetSeed(int seed)
    {
        random = new Random(seed);
    }

    // ��ȡһ���������
    public int GetRandomInt()
    {
        return random.Next();
    }

    // ��ȡһ��ָ����Χ���������
    public int GetRandomInt(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue);
    }

    // ��ȡһ�����������
    public float GetRandomFloat()
    {
        return (float)random.NextDouble();
    }

    // ��ȡһ��ָ����Χ�����������
    public float GetRandomFloat(float minValue, float maxValue)
    {
        return (float)random.NextDouble() * (maxValue - minValue) + minValue;
    }
}
