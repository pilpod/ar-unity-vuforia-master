using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDetection : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Image imageDetection;
    [Space]
    [SerializeField] private Color colorOn;
    [SerializeField] private Color colorOff;

    // Start is called before the first frame update
    void Start()
    {
        EnableDetection(false);
    }

    public void EnableDetection(bool enable)
    {
        imageDetection.color = enable ? colorOn : colorOff;
    }

    
}
