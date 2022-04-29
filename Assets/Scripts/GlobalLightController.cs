using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightController : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Light2D>().intensity = 0.02f;
    }
}
