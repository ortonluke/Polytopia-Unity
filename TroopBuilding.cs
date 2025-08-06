using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopBuilding : Structure
{
    private PopupPanel popupPanel;
    [SerializeField] private string buildingType;

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
