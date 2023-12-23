using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    public GameObject bomb;

    private PlayerInputAction controls;

    private void Awake()
    {
        controls = new PlayerInputAction();
        controls.GamePlay.Bomb.started += ctx =>
        {
            Instantiate(bomb, transform.position, transform.rotation);
        };
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
        //if(Input.GetKeyDown(KeyCode.O))
        //{
        //    Instantiate(bomb, transform.position, transform.rotation);
        //}
        
    }
}
