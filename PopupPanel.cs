using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPanel : MonoBehaviour
{
    [SerializeField] private RectTransform panelTransform;
    [SerializeField] private Transform contentArea; // Assign ContentArea here
    [SerializeField] private float slideSpeed = 500f;

    //Troops
    private List<GameObject> troopsUnlocked;
    [SerializeField] private GameObject warriorTroopPrefab;

    //Structures
    private List<GameObject> structuresUnlocked;
    [SerializeField] private GameObject farmPrefab;

    private bool isVisible = false;

    public void ShowTroops()
    {
        ClearContent();
        foreach (var icon in troopsUnlocked)
        {
            var btn = Instantiate(icon, contentArea);
        }
        SlideIn();
    }

    public void ShowStructures() 
    {
        ClearContent();
        foreach (var icon in structuresUnlocked)
        {
            var btn = Instantiate(icon, contentArea);
        }
        SlideIn();
    }

    private void ClearContent()
    {
        foreach (Transform child in contentArea)
        {
            Destroy(child.gameObject);
        }
    }

    private void SlideIn()
    {
        StopAllCoroutines();
        StartCoroutine(Slide(0)); // 0 = fully visible
        isVisible = true;
    }

    private void SlideOut()
    {
        StopAllCoroutines();
        StartCoroutine(Slide(-panelTransform.rect.height));
        isVisible = false;
    }

    private IEnumerator Slide(float targetY)
    {
        Vector2 anchoredPos = panelTransform.anchoredPosition;
        while (Mathf.Abs(anchoredPos.y - targetY) > 0.1f)
        {
            anchoredPos.y = Mathf.MoveTowards(anchoredPos.y, targetY, slideSpeed * Time.deltaTime);
            panelTransform.anchoredPosition = anchoredPos;
            yield return null;
        }
        anchoredPos.y = targetY;
        panelTransform.anchoredPosition = anchoredPos;
    }
}
