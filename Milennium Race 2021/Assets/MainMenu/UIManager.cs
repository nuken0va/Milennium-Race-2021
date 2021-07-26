using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Common.DoQuit();
    }

    public void StartGame()
    {
        GameInfo.selectedCar = GameObject.Find("CarSelector").GetComponent<CarSelector>().GetSelected().id;
        SceneManager.LoadScene(1) ;
    }
}
