using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleButtons : MonoBehaviour
{
    [SerializeField] private Transform arSession;
    public void ScaleUp()
    {
        Vector3 originalScale = arSession.localScale;
        Vector3 newScale = originalScale * 0.9f;
        arSession.transform.localScale = newScale;
    }

    public void ScaleDown()
    {
        Vector3 originalScale = arSession.localScale;
        Vector3 newScale = originalScale * 1.1f;
        arSession.transform.localScale = newScale;
    }
}
