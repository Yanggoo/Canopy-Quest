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
                // 在第一次访问时，使用默认种子创建实例
                instance = new RandomManager(Environment.TickCount);
            }
            return instance;
        }
    }

    // 设置随机数种子
    public void SetSeed(int seed)
    {
        random = new Random(seed);
    }

    // 获取一个随机整数
    public int GetRandomInt()
    {
        return random.Next();
    }

    // 获取一个指定范围的随机整数
    public int GetRandomInt(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue);
    }

    // 获取一个随机浮点数
    public float GetRandomFloat()
    {
        return (float)random.NextDouble();
    }

    // 获取一个指定范围的随机浮点数
    public float GetRandomFloat(float minValue, float maxValue)
    {
        return (float)random.NextDouble() * (maxValue - minValue) + minValue;
    }
}
