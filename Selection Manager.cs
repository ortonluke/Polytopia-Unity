using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private Tile lastHoveredTile;

    void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null && !Input.GetMouseButton(2))
        {
            Tile tile = hit.collider.GetComponent<Tile>();

            if (tile != null)
            {
                if (tile != lastHoveredTile)
                {
                    if (lastHoveredTile != null)
                    {
                        lastHoveredTile.ResetColor();
                    }

                    tile.SetFadedColor();
                    lastHoveredTile = tile;
                }
            }
        }
        else
        {
            if (lastHoveredTile != null)
            {
                lastHoveredTile.ResetColor();
                lastHoveredTile = null;
            }
        }
    }
}
