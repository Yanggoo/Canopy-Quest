using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image image;
    public float flashTime;
    public Color flashColor;

    private Color defaultFlashColor;
    // Start is called before the first frame update
    void Start()
    {
        defaultFlashColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlashScreen()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        image.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        image.color = defaultFlashColor;
    }
}
