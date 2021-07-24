using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelector : MonoBehaviour
{
    List<CarSelection> carSelections;
    int index = -1;
    public void Subscribe(CarSelection carSelection)
    {
        if (carSelections == null)
        {
            carSelections = new List<CarSelection>();
        }
        carSelections.Add(carSelection);
        if (index == -1)
        {
            Select(0);
        }
    }

    private void Select(int newIndex)
    {
        if (index != -1)
        {
            carSelections[index].gameObject.SetActive(false);
        }
        index = newIndex;
        carSelections[index].gameObject.SetActive(true);
    }
    public void MoveSelection(int shift)
    {
        print((index + shift + carSelections.Count) % carSelections.Count);
        Select((index + shift + carSelections.Count) % carSelections.Count);
        print(index);
    }
    public CarSelection GetSelected() 
    {
        return carSelections[index];
    }
}
