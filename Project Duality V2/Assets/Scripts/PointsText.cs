using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsText : MonoBehaviour
{
    [SerializeField]
    FloatSO Points;

    [SerializeField]
    TextMeshProUGUI PointsTXT;

    private void OnEnable()
    {
        Points.OnValueChange += UpdatePoints;
    }

    private void OnDisable()
    {
        Points.OnValueChange -= UpdatePoints;
    }


    void UpdatePoints(int Points)
    {
        PointsTXT.text = "Points: " + Points.ToString();
    }
}
