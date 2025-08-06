using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private Tile lastHoveredTile;
    private Vector2 lastClickPos;
    private int clickIndex = 0;

    private Clickable lastClicked;

    void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero);

        if (hits.Length > 0 && !Input.GetMouseButton(2))
        {
            // Sort hits by SpriteRenderer.sortingOrder descending
            System.Array.Sort(hits, (a, b) =>
            {
                SpriteRenderer srA = a.collider.GetComponent<SpriteRenderer>();
                SpriteRenderer srB = b.collider.GetComponent<SpriteRenderer>();

                int orderA = srA != null ? srA.sortingOrder : 0;
                int orderB = srB != null ? srB.sortingOrder : 0;

                return orderB.CompareTo(orderA); // higher order first
            });

            Debug.Log("hits:" + hits);

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
                bool topIsTroop = clickables[0] is Troop;
                bool hasBuildingBelow = clickables.Count > 1 && clickables[1] is Structure;
                bool buildingSpawnsTroops = hasBuildingBelow && clickables[1] is TroopBuilding;
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

    public Clickable GetLastClicked()
    {
        return lastClicked;
    }
}
