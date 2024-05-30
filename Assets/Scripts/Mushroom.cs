using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mushroom : MonoBehaviour
{
    public float shakeStrength;
    public float shakeDuration;
    public GameObject shadow;

    private void Start()
    {
        shadow.transform.parent = null;
    }

    public void Shake()
    {
        transform.DOShakeRotation(shakeDuration,shakeStrength,2,90);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.M)) 
        {
            Shake();
        }
    }
}
