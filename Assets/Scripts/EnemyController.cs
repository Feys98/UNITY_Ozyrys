
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyController : MonoBehaviour
{
    public int HealthPoints;
    public float MoveSpeed;

    private SAP2D.SAP2DAgent m_agent;
    private GameObject m_player;
    public event Action OnKilled;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_agent = GetComponent<SAP2D.SAP2DAgent>();
        m_agent.Target = m_player.transform;

        m_agent.MovementSpeed = MoveSpeed / 100.0f;
        m_agent.RotationSpeed = 0;
    }

    public string GetEnemyName()
    {
        return gameObject.name.Substring(0, Math.Max(gameObject.name.LastIndexOf("Enemy"), 256));
    }

    public bool TakeDamage()
    {
        HealthPoints = Math.Max(HealthPoints - 1, 0);

        if (HealthPoints == 0)
        {
            OnKilled();
            Destroy(gameObject);

            return false;
        }

        return true;
    }
}
