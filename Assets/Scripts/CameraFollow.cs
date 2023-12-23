using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-100)]
public class CameraFollow : MonoBehaviour
{
    static public bool isInMapMod=false;
    public Transform target;
    public float smoothing;

    public Vector2 minPosition;
    public Vector2 maxPosition;
    public Camera mainCamera;
    // Start is called before the first frame update

    private PlayerInputAction controls;
    [SerializeField]
    private float mapCameraSize;
    [SerializeField]
    private float oringinalCameraSize;

    private void Awake()
    {
        oringinalCameraSize = mainCamera.orthographicSize;
        controls = new PlayerInputAction();
        controls.GamePlay.Map.started += ctx => {
            if (mainCamera.orthographicSize!= oringinalCameraSize)
            {
                mainCamera.orthographicSize = oringinalCameraSize;
                isInMapMod = false;
            }
            else
            {
                mainCamera.orthographicSize = mapCameraSize;
                isInMapMod = true;
            }
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

    void Start()
    {
        GameController.cameraShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }
    private void LateUpdate()
    {
        if (target != null)
        {
            if(transform.position != target.position)
            {
                Vector3 targetPosition = target.position;
                targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            }
        }
    }

    public void SetCameraPositionLimit(Vector2 minPos, Vector2 maxPos)
    {
        minPosition = minPos;
        maxPosition = maxPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
