using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleHit : MonoBehaviour
{
    public GameObject sickle;

    private PlayerInputAction controls;

    private void Awake()
    {
        controls = new PlayerInputAction();
        controls.GamePlay.Sickle.started += ctx => { Instantiate(sickle, transform.position, transform.rotation); };
    }

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }
    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    Instantiate(sickle, transform.position, transform.rotation);
        //}
    }

}
