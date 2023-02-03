using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraSystem : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    public float cameraSpeed;
    public float rotateSpeed;
    public float panSpeedSlowdown;
    public float zoomSpeed;
    public float zoomAmount;
    public float maxZoom = 50f;
    public float minZoom = 5f;
    public float panYSlowDown = 10f;
    //Edge scroll
    private int edgeScrollSize = 20;
    public bool useEdgeScroll = false;
    //Panning
    public bool useDragMoving = false;
    private bool isPanning;
    private Vector2 lastMousePosition;

    private Vector3 followOffset;
    [Header("Boundaries")]
    public float maxX;
    public float maxY;
    public float maxZ;
    public float minX;
    public float minY;
    public float minZ;

    private bool isPanMovingY = false;

    private void Awake()
    {
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    void Update() {
        if (!PlayerStats.gameOver && !PlayerStats.inMainMenu)
        {
            RestrictCameraMovementArea();
            MoveCamera();
            RotateCamera();
            ZoomCamera();
            PanMoveCameraY();
            if (useEdgeScroll)
                EdgeScroll();
            if (useDragMoving)
                DragMoveCamera();
        }

    }
    void MoveCamera() {
        //Move input
        Vector3 inputDirection = new Vector3(0, 0, 0);

        if (Input.GetKey("w")) inputDirection.z = +1f;
        if (Input.GetKey("s")) inputDirection.z = -1f;
        if (Input.GetKey("a")) inputDirection.x = -1f;
        if (Input.GetKey("d")) inputDirection.x = +1f;

        //Moving
        Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
        transform.position += moveDirection * cameraSpeed * Time.deltaTime;
    }
    void EdgeScroll() {
        Vector3 inputDirection = new Vector3(0, 0, 0);

        if (Input.mousePosition.x < edgeScrollSize) inputDirection.x = -1f;
        if (Input.mousePosition.y < edgeScrollSize) inputDirection.z = -1f;
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDirection.x = +1f;
        if (Input.mousePosition.y > Screen.height - edgeScrollSize) inputDirection.z = +1f;

        Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
        transform.position += moveDirection * cameraSpeed * Time.deltaTime;
    }

    void RotateCamera() {
        float rotateDirection = 0f;
        if (Input.GetKey("q")) rotateDirection = +1;
        if (Input.GetKey("e")) rotateDirection = -1;
        transform.eulerAngles += new Vector3(0, rotateDirection * rotateSpeed * Time.deltaTime, 0);
    }
    void ZoomCamera()
    {
        Vector3 zoomDirection = followOffset.normalized;
        //Mouse scrolling zoom
        if (Input.mouseScrollDelta.y > 0)
        {
            followOffset -= zoomDirection * zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            followOffset += zoomDirection * zoomAmount;
        }
        //For quick zoom in and out there are z and x
        if (Input.GetKey("z"))
        {
            followOffset -= zoomDirection * zoomAmount;
        }
        if (Input.GetKey("x"))
        {
            followOffset += zoomDirection * zoomAmount;
        }

        if (followOffset.magnitude < minZoom)
        {
            followOffset = zoomDirection * minZoom;
        }
        if (followOffset.magnitude > maxZoom)
        {
            followOffset = zoomDirection * maxZoom;
        }
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = 
            Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);
    }
    void DragMoveCamera()
    {
        Vector3 inputDirection = new Vector3(0, 0, 0);
        //Panning
        if (Input.GetMouseButtonDown(1))
        {
            isPanning = true;
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isPanning = false;
        }
        if (isPanning)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;
            inputDirection.x = -mouseMovementDelta.x / panSpeedSlowdown;
            inputDirection.z = -mouseMovementDelta.y / panSpeedSlowdown;

            Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
            transform.position += moveDirection * cameraSpeed * Time.deltaTime;
        }
    }
    void PanMoveCameraY()
    {
        Vector3 inputDirection = new Vector3(0, 0, 0);
        if (Input.GetMouseButtonDown(2))
        {
            isPanMovingY = true;
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(2))
        {
            isPanMovingY = false;
        }
        if (isPanMovingY)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;
            inputDirection.y = mouseMovementDelta.y / panYSlowDown;
            //print(inputDirection.y);
            followOffset.y += inputDirection.y * Time.deltaTime;

            followOffset.y = Mathf.Clamp(followOffset.y, minY, maxY);

            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
        Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);
        }

    }
    void RestrictCameraMovementArea()
    {
        transform.position = new Vector3 (
            Mathf.Clamp(transform.position.x, minX, maxX),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minZ, maxZ)
            );
    }
}
