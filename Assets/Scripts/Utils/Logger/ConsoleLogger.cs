using System;
using UnityEngine;

namespace UnityUI.Utils
{
    public static class ConsoleLogger
    {
        public static void Log(object message)
        {
            Debug.Log(message);
        }
        
        public static void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }
        
        public static void LogError(object message)
        {
            Debug.LogError(message);
        }
        
        public static void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }
    }
}