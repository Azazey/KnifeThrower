using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualitySettings : MonoBehaviour
{
    void Start()
    {
        UnityEngine.QualitySettings.vSyncCount = 0;
        UnityEngine.QualitySettings.shadows = ShadowQuality.Disable;
        UnityEngine.QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        UnityEngine.QualitySettings.antiAliasing = 0;
        UnityEngine.QualitySettings.shadowCascades = 0;
        Application.targetFrameRate = 1000;
    }
}
