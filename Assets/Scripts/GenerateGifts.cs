using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGifts : MonoBehaviour
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

    private void OnDestroy()
    {
        Instantiate(objects[Random.Range(0, objects.Length)], transform.position, Quaternion.identity);
    }
}
