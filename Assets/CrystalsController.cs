using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrystalsController : MonoBehaviour
{
    private int m_collectiblesCount;
    private int m_currentCount;

    // Start is called before the first frame update
    void Start()
    {
        m_collectiblesCount = GameObject.FindGameObjectsWithTag("Collectible").Length;
    }

    public void UpdateCollectiblesText()
    {
        m_currentCount++;

        GetComponent<TMP_Text>().SetText($"{Math.Floor(1.0 * m_currentCount / m_collectiblesCount * 100)}%");
    }
}
