using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    private TabButton selectedTab;
    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
        if (!selectedTab)
        {
            OnTabSelected(button);
        }
    }

    void ResetTabs()
    {
        foreach (var button in tabButtons)
        {
            if (!selectedTab || selectedTab != button)
            {
                button.bg.color = button.Idle;
            }
            else if (selectedTab == button)
            {
                button.bg.color = button.Selected;
            }
        }
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab && selectedTab != button)
        {
            button.bg.color = button.Hover;
        }
    }
    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }
    public void OnTabSelected(TabButton button)
    {
        if (selectedTab && selectedTab.Tab)
        {
            selectedTab.Tab.SetActive(false);
        }

        selectedTab = button;

        ResetTabs();

        button.bg.color = button.Selected;
        if (selectedTab.Tab)
        {
            selectedTab.Tab.SetActive(true);
        }

    }
}
