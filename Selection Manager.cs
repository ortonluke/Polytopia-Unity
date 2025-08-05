using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private Tile lastHoveredTile;
    private Vector2 lastClickPos;
    private int clickIndex = 0;

    void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero);

        if (hits.Length > 0 && !Input.GetMouseButton(2))
        {
            Tile tile = null;
            List<Clickable> clickables = new List<Clickable>();

            // Gather hits
            foreach (var h in hits)
            {
                if (tile == null) // First tile found for hover
                    tile = h.collider.GetComponent<Tile>();

                Clickable c = h.collider.GetComponent<Clickable>();
                if (c != null)
                    clickables.Add(c);
            }

            // --- Handle hover (topmost tile) ---
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

            // --- Handle click ---
            if (Input.GetMouseButtonDown(0) && clickables.Count > 0)
            {
                // Cycle if same spot clicked again
                if (mouseWorldPos == lastClickPos)
                {
                    clickIndex = (clickIndex + 1) % clickables.Count;
                }
                else
                {
                    lastClickPos = mouseWorldPos;
                    clickIndex = 0;
                }

                clickables[clickIndex].OnClick();
            }
        }
        else
        {
            // Reset hover when nothing is under mouse
            if (lastHoveredTile != null)
            {
                lastHoveredTile.ResetColor();
                lastHoveredTile = null;
            }
        }
    }
}
