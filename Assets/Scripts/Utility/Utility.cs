using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Utility
{
}

public static class CameraExtension
{
    public static float GetPixelPerMeter(this Camera camera)
    {
        return (float)camera.pixelHeight / 2f / Mathf.Tan(camera.fieldOfView / 2 / 180 * Mathf.PI);
        //return camera.pixelHeight / 2f / Mathf.Tan(camera.fieldOfView / 360 * Mathf.PI);
    }
}

public class MonobehaviourExtension : MonoBehaviour
{
    public bool isDebug = false;
    public void Log(string format, params object[] objs)
    {
        if (isDebug)
        {
            Debug.Log(ToString() + string.Format(format, objs));
        }
    }
    public void LogError(string format, params object[] objs)
    {
        if (isDebug)
        {
            Debug.Log(ToString() + string.Format(format, objs));
        }
    }
    public void LogWarning(string format, params object[] objs)
    {
        if (isDebug)
        {
            Debug.Log(ToString() + string.Format(format, objs));
        }
    }
}
