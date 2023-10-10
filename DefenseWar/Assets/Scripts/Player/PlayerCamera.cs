using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float moveSpeed;

    [Space(15)]
    [Header("Zoom")]
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float min_ZoomValue;
    [SerializeField] private float max_ZoomValue;
    private void Update()
    {
        CameraZoom();
        MoveInputCheck();
    }

    private void CameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (scroll != 0)
        {
            playerCamera.fieldOfView += -scroll;
            playerCamera.fieldOfView = Mathf.Clamp(playerCamera.fieldOfView, min_ZoomValue, max_ZoomValue);
        }
    }

    private void MoveInputCheck()
    {
        var mousePos = Input.mousePosition;
        if (mousePos.x <= 0 || mousePos.x >= Screen.width ||
            mousePos.y <= 0 || mousePos.y >= Screen.height)
        {
            var centerPos = new Vector3(Screen.width * 0.5f, 0, Screen.height * 0.5f);
            var dir = (mousePos - centerPos).normalized;

            CameraMovement(new Vector3(dir.x, 0, dir.y));
            return;
        }

        var moveInput = new Vector3(Input.GetAxis("Horizontal"), 0,
                                    Input.GetAxis("Vertical"));
        if (moveInput != Vector3.zero) CameraMovement(moveInput);
    }

    private void CameraMovement(Vector3 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, Utils.MIN_CAMERA_X, Utils.MAX_CAMERA_X),
            transform.position.y, Mathf.Clamp(transform.position.z, Utils.MIN_CAMERA_Z, Utils.MAX_CAMERA_Z));
    }
}
