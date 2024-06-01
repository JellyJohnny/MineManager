using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TileSelector : MonoBehaviour
{
    public GameObject cursor;
    Vector3 cursorPosition = Vector3.zero;
    public float newScale;
    public float scaleDuration;
    public Camera playerCam;

    Vector3 worldPosition;


    private void Start()
    {
        StartCoroutine(PulseCursor());
    }

    private void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        mousePos.z = playerCam.nearClipPlane;
        worldPosition = playerCam.ScreenToWorldPoint(mousePos);

        if (playerCam != null)
        {
            cursor.transform.position = new Vector3(Mathf.RoundToInt(worldPosition.x * 2) / 2, Mathf.RoundToInt(worldPosition.y * 2) / 2,worldPosition.z);
        }
    }

    IEnumerator PulseCursor()
    {
        cursor.transform.DOScale(newScale, scaleDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(scaleDuration);
        cursor.transform.DOScale(1f, scaleDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(scaleDuration);
        StartCoroutine(PulseCursor());
    }
}
