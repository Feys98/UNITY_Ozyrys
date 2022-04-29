using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToObjPosition : MonoBehaviour
{
    private GameObject Player;
    private int OrderInLayerUpValue = 6;
    private int OrderInLayerDownValue = 4;
    public float offset = 0;
    private SpriteRenderer spritePlayer;
    //private float playersHight;

    private void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");

        spritePlayer = GetComponent<SpriteRenderer>();

        //rtPlayer = (RectTransform)Player.transform;
        //playersHight = rtPlayer.rect.height;

        //float playersHight = Player.GetComponent<Renderer>().bounds.size.y;
        //Debug.Log(playersHight.ToString());

    }
    void Update()
    {

        spritePlayer.sortingOrder = transform.position.y + offset <= Player.transform.position.y ? OrderInLayerUpValue : OrderInLayerDownValue;
    }
}