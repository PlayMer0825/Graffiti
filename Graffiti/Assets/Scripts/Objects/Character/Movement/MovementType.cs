using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementType
{
    protected Transform e_standardTransform = null;
    protected Vector3 i_inputDir = Vector3.zero;

    #region 
    public virtual Vector3 MovementUpdate() { return Vector3.zero; }
    public virtual Vector3 RotationUpdate() { return Vector3.zero; }

    #endregion

    #region 
    public virtual void SetInputDirection(Vector3 rawInput) { }

    #endregion
}

public class TPSMovementType : MovementType {

}