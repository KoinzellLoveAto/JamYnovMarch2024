using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RakaEngine.RDebug
{

    /// <summary>
    /// blue = gameflow
    /// yellow = state
    /// green = panels
    /// red = pool
    /// </summary>
    public static class RakaDebug
    {
        public static void Log(object a_message, string a_colorText = "white")
        {
            Debug.Log($"<color={a_colorText}> {a_message} </color>");
        }

        public static void LogWarning(object a_message, string a_colorText = "white")
        {
            Debug.LogWarning($"<color={a_colorText}> {a_message} </color>");
        }
        public static void LogError(object a_message, string a_colorText = "white")
        {
            Debug.LogError($"<color={a_colorText}> {a_message} </color>");
        }
    }
}
