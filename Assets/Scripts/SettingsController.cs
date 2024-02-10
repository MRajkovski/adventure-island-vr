using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SettingsController : MonoBehaviour
{
    public bool postProcessingEnabled;
    public Volume postProcessing;

    // Start is called before the first frame update
    void Start()
    {
        if (!postProcessingEnabled && postProcessing)
        {
            postProcessing.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
