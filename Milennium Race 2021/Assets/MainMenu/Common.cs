using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : MonoBehaviour
{
    public static void DoQuit()
    {
        Application.Quit();
    }
    public static void Windowed(bool mode)
    {
        print(mode);
        Screen.fullScreen = !mode;
    }

    public static void SetResolution(int index)
    {
        Resolution resolution = Screen.resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
        print(Screen.currentResolution);
    }
}
