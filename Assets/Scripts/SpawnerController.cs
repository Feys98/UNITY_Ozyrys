using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    void Start()
    {
        // hide object (used in design mode and later in scripts)
        gameObject.transform.position -= new Vector3(0, 0, -1);
    }
}
