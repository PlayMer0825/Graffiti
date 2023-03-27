using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {
    private static CursorLockMode i_prevMode = CursorLockMode.None;

    public static void ChangeCursorModeTo(CursorLockMode cursorLockMode) {
        if(Cursor.lockState == cursorLockMode)
            return;

        i_prevMode = Cursor.lockState;
        Cursor.lockState = cursorLockMode;
    }

    public static void ChangeCursorModePrev() {
        if(i_prevMode == Cursor.lockState)
            return;

        Cursor.lockState = i_prevMode;
    }
}
