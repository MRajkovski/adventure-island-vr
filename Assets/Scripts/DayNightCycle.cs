using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    [Range(0, 24)]
    public float timeOfDay;
    public float orbitSpeed = 1.0f;
    public Light sun;
    public Light moon;
    public Volume skyVolume;
    public Material skyMat;
    public LensFlareDataSRP daySRP;
    public LensFlareDataSRP sunsetSRP;
    public bool lensFlaresOn;
    public bool basicFogOn;
    private float sunRotation;
    private float moonRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (!lensFlaresOn)
        {
            sun.GetComponent<LensFlareComponentSRP>().enabled = false;
            moon.GetComponent<LensFlareComponentSRP>().enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {  
        timeOfDay += Time.deltaTime * orbitSpeed;
        if (timeOfDay > 24)
        {
            timeOfDay = 0;
        }
        UpdateTime();
    }
    private void OnValidate()
    {
        UpdateTime();
    }
    private void UpdateTime()
    {
        float alpha = timeOfDay / 24.0f;
        sunRotation = Mathf.Lerp(-90f, 270F, alpha);
        moonRotation = Mathf.Lerp(-270f, 90f, alpha);

        CheckTime();
        CheckOrbitSpeed();

        sun.transform.rotation = Quaternion.Euler(sunRotation, -140.0f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation - 5.5f, -140.0f, 0);

        float exposureLerp = Mathf.PingPong(alpha + 0.3f, 0.8f);
        float thicknessLerp = Mathf.PingPong(alpha + 0.3f, 0.8f);
        skyMat.SetFloat("_Exposure", exposureLerp);
        skyMat.SetFloat("_AtmosphereThickness", thicknessLerp);

        if(lensFlaresOn)
        {
            FlareControl();
        }
        if(basicFogOn)
        {
            FogControl();
        }
        
    }
    private void CheckTime()
    {
        if (sunRotation <= 185.5f && sunRotation >= 0f)
        {
            sun.enabled = true;
            moon.enabled = false;
        }
        else
        {
            sun.enabled = false;
            moon.enabled = true;
        }
    }
    private void CheckOrbitSpeed()
    {
        if (sun.enabled == false && moon.enabled == true)
        {
            orbitSpeed = 0.02f;
        }
        else
        {
            orbitSpeed = 0.01f;
        }
    }
    private void FlareControl()
    {
        if (timeOfDay <= 18.1f && timeOfDay >= 5.9f)
        {
            if (lensFlaresOn)
            {
                sun.GetComponent<LensFlareComponentSRP>().enabled = true;
                moon.GetComponent<LensFlareComponentSRP>().enabled = false;
            }
            if (timeOfDay > 18f)
            {
                if (lensFlaresOn)
                {
                    sun.GetComponent<LensFlareComponentSRP>().lensFlareData = sunsetSRP;
                }
            }
            else
            {
                if (lensFlaresOn)
                {
                    sun.GetComponent<LensFlareComponentSRP>().lensFlareData = daySRP;
                }
            }
        }
        else
        {
            if (lensFlaresOn)
            {
                sun.GetComponent<LensFlareComponentSRP>().enabled = false;
            }
        }
    }

    private void FogControl()
    {
        float fogAlpha = timeOfDay / 24.0f;
        Color lightYellow = new Color(0.3f, 0.3f, 0.21f);
        Color darkBlue = new Color(0.08f, 0.11f, 0.39f);
        if (timeOfDay <= 18.1f && timeOfDay >= 5.9f)
        {
            RenderSettings.fogColor = Color.Lerp(lightYellow, darkBlue, fogAlpha);
            if (timeOfDay > 18f)
            {
                skyMat.SetColor("_SkyTint", new Color(1f, 0.48f, 0));
            }
            else
            {
                skyMat.SetColor("_SkyTint", new Color(0.5f, 0.5f, 0.5f));
            }
        }
        else
        { 
            skyMat.SetColor("_SkyTint", new Color(0.5f, 0.5f, 0.5f));
            RenderSettings.fogColor = Color.Lerp(darkBlue, lightYellow, fogAlpha);
        }
    }

}