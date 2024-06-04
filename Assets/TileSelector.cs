using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TileSelector : MonoBehaviour
{
    public GameObject cursor;
    public GameObject tileselector;
    Vector3 cursorPosition = Vector3.zero;
    public float newScale;
    public float scaleDuration;
    public float cursorStickyness;
    public Camera playerCam;

    Vector3 worldPosition;


    private void Start()
    {
        StartCoroutine(PulseCursor());
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        mousePos.z = playerCam.nearClipPlane;
        worldPosition = playerCam.ScreenToWorldPoint(mousePos);

        if (playerCam != null)
        {
            transform.position = worldPosition;
            tileselector.transform.position = new Vector3(Mathf.RoundToInt(transform.position.x * 2) / 2, Mathf.RoundToInt(transform.position.y * 2) / 2,worldPosition.z);
        }
    }

    IEnumerator PulseCursor()
    {
        tileselector.transform.DOScale(newScale, scaleDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(scaleDuration);
        tileselector.transform.DOScale(1f, scaleDuration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(scaleDuration);
        StartCoroutine(PulseCursor());
    }
}
