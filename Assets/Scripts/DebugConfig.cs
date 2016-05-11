using UnityEngine;

public class DebugConfig : MonoBehaviour
{
    public static bool isDebug;

    public static void print(string message)
    {
        if (isDebug)
            MonoBehaviour.print(message);
    }
}