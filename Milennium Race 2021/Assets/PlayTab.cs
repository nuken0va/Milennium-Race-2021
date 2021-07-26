using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTab : MonoBehaviour
{
    public UIManager uIManager;
    public Button lButton, rButton;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) lButton.onClick.Invoke();
        if (Input.GetKeyDown(KeyCode.RightArrow)) rButton.onClick.Invoke();
        if (Input.GetKeyDown(KeyCode.Return)) uIManager.StartGame(); 
    }
}
