using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Runtime : MonoBehaviour
{
    public TextMeshProUGUI runtime;
    private float elapsedTime = 0f;

    void Start()
    {
        runtime.text = "Elapsed time: " + elapsedTime;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateTime();
    }
    void UpdateTime()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        runtime.text = "Elapsed time: " + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
