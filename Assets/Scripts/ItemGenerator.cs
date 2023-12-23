using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif
public class ItemGenerator : MonoBehaviour
{
    [SerializeField]
    protected float itemSpeed=10.0f;
    [SerializeField]
    protected float intervalTime=0.1f;
    [SerializeField]
    public List<Item> itemList;
    [SerializeField]
    protected int totalValue;

    [Serializable]
    public class Item : IComparable<Item>
    {
        public GameObject prefab;
        public int value;
        public float possibility;

        // 构造函数，用于创建具体的物品实例
        public Item(GameObject prefab, int value, float possibility)
        {
            this.prefab = prefab;
            this.value = value;
            this.possibility = possibility;
        }

        public int CompareTo(Item other)
        {
            if (other == null)
            {
                // 如果比较的对象为null，放在列表末尾
                return 1;
            }

            // 按照概率升序排序
            return this.possibility.CompareTo(other.possibility);
        }
    }
    protected void GenerateItems(bool needGenerateWithCurve)
    {
        itemList.Sort();
        float possibility = RandomManager.Instance.GetRandomFloat(0, 1.0f);
        foreach (var item in itemList)
        {
            var itemGenenrateList = new List<GameObject>();
            if (item.possibility >= possibility && item.value < totalValue)
            {
                for (int i = 0; i < totalValue / item.value; i++)
                {
                    itemGenenrateList.Add(item.prefab);
                }
                totalValue = totalValue % item.value;
            }
            if (needGenerateWithCurve)
            {
                StartCoroutine(GenerateItemsWithCurve(itemGenenrateList));
            }
            else
            {
                StartCoroutine(GenerateItemsStightly(itemGenenrateList));
            }
        }
    }

    IEnumerator GenerateItemsWithCurve(List<GameObject> itemsToGenerate)
    {
        WaitForSeconds wait = new WaitForSeconds(intervalTime);
        for (int i = 0; i < itemsToGenerate.Count; i++)
        {
            GameObject gb = Instantiate(itemsToGenerate[i], GetComponent<SpriteRenderer>().bounds.center, Quaternion.identity);
            Vector2 randomDirection = new Vector2(RandomManager.Instance.GetRandomFloat(-0.3f, 0.3f), 1.0f);
            gb.GetComponent<Rigidbody2D>().velocity = randomDirection * itemSpeed;
            yield return wait;
        }
    }

    IEnumerator GenerateItemsStightly(List<GameObject> itemsToGenerate)
    {
        WaitForSeconds wait = new WaitForSeconds(intervalTime);
        for (int i = 0; i < itemsToGenerate.Count; i++)
        {
            GameObject gb = Instantiate(itemsToGenerate[i], GetComponent<SpriteRenderer>().bounds.center, Quaternion.identity);
            //Vector2 randomDirection = new Vector2(RandomManager.Instance.GetRandomFloat(-0.3f, 0.3f), 1.0f);
            //gb.GetComponent<Rigidbody2D>().velocity = randomDirection * itemSpeed;
            yield return wait;
        }
    }

}


