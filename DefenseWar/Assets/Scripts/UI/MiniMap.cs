using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMap : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Camera miniMapCamera;
    [SerializeField] private RectTransform miniMap;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(miniMap,
            eventData.position, eventData.pressEventCamera, out Vector2 localCursor)) return;

        var valueX = localCursor.x / miniMap.rect.width;
        var valueZ = localCursor.y / miniMap.rect.height;

        var posX = (Utils.MAX_CAMERA_X - Utils.MIN_CAMERA_X) * valueX;
        var posZ = (Utils.MAX_CAMERA_Z - Utils.MIN_CAMERA_Z) * valueZ;

        PlayerCamera.Inst.PointMove(new Vector3(Utils.MIN_CAMERA_X + posX, 0,
            Utils.MIN_CAMERA_Z + posZ));

        //var value = eventPos / (Vector2)transform.position;

        //var xDis = Mathf.Abs(Utils.MAX_CAMERA_X + Utils.MIN_CAMERA_X) * value.x;
        //var yDis = Mathf.Abs(Utils.MAX_CAMERA_Z + Utils.MIN_CAMERA_Z) * value.y;

        //var movePosX = Utils.MIN_CAMERA_X - xDis;
        //var movePosY = Utils.MIN_CAMERA_X - yDis;

        //PlayerCamera.Inst.PointMove(new Vector3(movePosX, 0f, movePosY)); 
    }
}
