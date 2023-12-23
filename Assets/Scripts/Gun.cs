using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform muzzleTransform;

    private Vector2 gunDirection;
    private Vector3 mousePosition;
    private Camera camera;
    private new SpriteRenderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        gunDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        if (gunDirection.x > 0.0f)
        {
            renderer.flipY = false;
        }
        else
        {
            renderer.flipY = true;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(bullet, muzzleTransform.position, Quaternion.Euler(transform.eulerAngles));
        }
    }
}
