using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ResolutionDropdown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var dd = GetComponent<Dropdown>();
        var resolutions = Screen.resolutions;
        var options = new List<string>();
        int currentResolution = 0; 
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString() + " " + resolutions[i].refreshRate.ToString() + "Hz";
            if ((Screen.fullScreen && Screen.currentResolution.refreshRate == resolutions[i].refreshRate || !Screen.fullScreen) &&
                Screen.width == resolutions[i].width && Screen.height == resolutions[i].height )
            { 
                currentResolution = i;
            }
            options.Add(option);
        }
        dd.AddOptions(options);
        dd.value = currentResolution;
        dd.RefreshShownValue();
    }
}
