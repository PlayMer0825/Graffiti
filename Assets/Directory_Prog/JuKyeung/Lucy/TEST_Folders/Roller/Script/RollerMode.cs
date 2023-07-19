using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RollerMode : MonoBehaviour
{
    [Header("롤러 마우스 포인트")]
    public Texture2D rollerCursur;


    private void Start()
    {
        RollerModeStart();
    }

    public void RollerModeStart()
    {
        Cursor.SetCursor(rollerCursur, Vector2.zero, CursorMode.Auto);
    }


}

