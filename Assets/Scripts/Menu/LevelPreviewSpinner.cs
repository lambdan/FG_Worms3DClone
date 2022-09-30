using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPreviewSpinner : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * Time.fixedDeltaTime * 10);
    }
}
