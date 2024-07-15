using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{

    private bool wingsUp;
    
    [SerializeField] private GameObject wingsUpImage;

    [SerializeField] private GameObject wingsDownImage;

    [SerializeField] private Animation clickableAnimation;
    
    [SerializeField] private GameObject canvas;

    [SerializeField] private GameObject pointPrefab;

    [SerializeField] private GameObject coinPrefab;

    private void Start()
    {
        WingsManagment();
    }

    public void Click()
    {
        InstantiateFeedbackObjects();

        clickableAnimation.Stop();
        
        clickableAnimation.Play();

        wingsUp = !wingsUp;

        WingsManagment();

        GameManager.Instance.OnClick();
    }


    private void InstantiateFeedbackObjects()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, Camera.main, out localPoint);

        // Convert the local point to a world position
        Vector3 worldPosition = canvas.transform.TransformPoint(localPoint);
        print(worldPosition);
        // Instantiate the object at the calculated position
        Instantiate(pointPrefab, worldPosition, Quaternion.identity, canvas.transform);

        Instantiate(coinPrefab, worldPosition, Quaternion.identity, canvas.transform);

    }

    private void WingsManagment()
    {
        if (wingsUp)
        {
            wingsUpImage.SetActive(true);
            wingsDownImage.SetActive(false);
        }
        else
        {
            wingsUpImage.SetActive(false);
            wingsDownImage.SetActive(true);
        }
    }
}
