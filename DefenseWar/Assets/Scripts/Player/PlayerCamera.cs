using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : Singleton<PlayerCamera>
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lookZvalue;

    [Space(15)]
    [Header("Zoom")]
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float min_ZoomValue;
    [SerializeField] private float max_ZoomValue;
    private int headQuaterPoint = 0;
    private Vector3 savePoint;

    private void Update()
    {
        CameraZoom();
        MoveInputCheck();
        CameraPointMove();
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
            var centerPos = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            var dir = ((Vector2)mousePos - centerPos).normalized;

            CameraMovement(new Vector3(dir.x, 0, dir.y));
            return;
        }

        var moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0,
                                    Input.GetAxisRaw("Vertical"));
        if (moveInput != Vector3.zero) CameraMovement(moveInput);
    }

    private void CameraPointMove()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PointMove(Player.Inst.finalHeadQuarters.GetCenterPos());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (headQuaterPoint >= Player.Inst.headQuarters.Count) headQuaterPoint = 0;

            if(Player.Inst.headQuarters.Count == 0)
            {
                PointMove(Player.Inst.finalHeadQuarters.GetCenterPos());
            }
            else
            {
                PointMove(Player.Inst.headQuarters[headQuaterPoint].GetCenterPos());
                headQuaterPoint++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PointMove(Player.Inst.GetUnitPos());
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (savePoint == Vector3.zero)
            {
                PointMove(Player.Inst.finalHeadQuarters.GetCenterPos());
            }
            else
            {
                PointMove(savePoint, false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            savePoint = transform.position;
        }
    }

    public void PointMove(Vector3 pos, bool isLook = true)
    {
        if (isLook) pos.z -= lookZvalue;

        pos.y = transform.position.y;
        transform.position = pos;
    }

    private void CameraMovement(Vector3 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, Utils.MIN_CAMERA_X, Utils.MAX_CAMERA_X),
            transform.position.y, Mathf.Clamp(transform.position.z, Utils.MIN_CAMERA_Z, Utils.MAX_CAMERA_Z));
    }
}
