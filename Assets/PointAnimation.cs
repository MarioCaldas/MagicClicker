using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAnimation : MonoBehaviour
{
    [SerializeField] private Animation clickableAnimation;

    private void Awake()
    {
        DisableRaycastTarget(gameObject);
    }

    private void Start()
    {
        PlayAnimation();
    }
    public void PlayAnimation()
    {
        clickableAnimation.Play();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    private void DisableRaycastTarget(GameObject obj)
    {
        // Disable raycast target for all UI components in the object
        foreach (var graphic in obj.GetComponentsInChildren<UnityEngine.UI.Graphic>())
        {
            graphic.raycastTarget = false;
        }
    }
}
