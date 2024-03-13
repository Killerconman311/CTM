using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offSet;
    public void UpdateHealthBar(float currentValue)
    {
        slider.value = currentValue;
    }
 

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.position + offSet;
    }
}
