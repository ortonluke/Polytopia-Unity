using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capital : Clickable
{
    private PopupPanel popupPanel;

    public override void OnClick()
    {
        //Show Troop Panel
        if (popupPanel != null)
        {
            popupPanel.ShowTroops();
        }
    }

    private void Start()
    {
        popupPanel = FindObjectOfType<PopupPanel>();
    }
}
