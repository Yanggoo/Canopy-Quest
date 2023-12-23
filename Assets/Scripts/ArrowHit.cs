using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    public GameObject arrowPrefab;

    private PlayerInputAction controls;
    private void Awake()
    {
        controls = new PlayerInputAction();
        controls.GamePlay.ArrowHit.started += ctx => {
            Shoot();
        };
    }
    private void Shoot()
    {
        Instantiate(arrowPrefab, transform.position, transform.rotation);
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
        
    }

}
