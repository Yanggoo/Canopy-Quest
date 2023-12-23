using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnityTime : MonoBehaviour
{
    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeleteByCoroutine()
    {
        for(int i = 0; i < objects.Length; i++)
        {
            StartCoroutine(DeleteObj(i, i + 1));
        }
    }

    IEnumerator DeleteObj(int i,float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(objects[i]);
    }

}
