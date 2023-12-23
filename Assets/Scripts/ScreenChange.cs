using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChange : MonoBehaviour
{
    public GameObject img1;
    public GameObject img2;

    private Animator anim;
    public float imageChangeDelayTime;
    private bool hasImageChanged;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hasImageChanged = false;
    }

    // Update is called once per frame
    public void ChangeImage()
    {
        if (hasImageChanged)
        {
            return;
        }
        hasImageChanged = true;
        anim.SetBool("ChangeToWhite", true);
        Invoke("SetImage", imageChangeDelayTime);

    }
    void SetImage()
    {
        if (img1)
            img1.SetActive(false);
        if (img2)
            img2.SetActive(true);
    }
}
