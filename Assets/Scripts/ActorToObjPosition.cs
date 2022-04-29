using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorToObjPosition : MonoBehaviour
{
    private GameObject Obstacle;
    private int OrderInLayerUpValue = 7;
    private int OrderInLayerDownValue = 3;
    private SpriteRenderer spriteActor;
    private RectTransform rtActor;
    private float actorHight;
    public GameObject FindClosestObstacle()
    {
        GameObject[] obstacles;
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in obstacles)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }


    // Start is called before the first frame update
    void Start()
    {


        spriteActor = GetComponent<SpriteRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        Obstacle = FindClosestObstacle();

        spriteActor.sortingOrder = Obstacle.transform.position.y <= transform.position.y  ? OrderInLayerDownValue : OrderInLayerUpValue;
    }
}