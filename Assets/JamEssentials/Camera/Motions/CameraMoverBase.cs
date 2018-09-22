using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraMoverBase : MonoBehaviour
{
    public virtual void ApplyMovement(Transform camTransform, Camera camera){
        return;
    }
}
