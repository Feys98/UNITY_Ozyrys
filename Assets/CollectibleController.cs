using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private int m_healthPoints = 4;
    private int m_currentSprite = 0;
    public Sprite[] Sprites;
    public Color DestructColor;
    public event Action OnCollect;
    private GameObject m_player;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Sprites[m_currentSprite];
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(nameof(TakeDamageCoroutine));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StopCoroutine(nameof(TakeDamageCoroutine));
        }
    }

    public IEnumerator TakeDamageCoroutine()
    {
        var renderer = GetComponent<SpriteRenderer>();
        while (TakeDamage())
        {
            renderer.color = DestructColor;
            yield return new WaitForSeconds(.1f);
            renderer.sprite = Sprites[++m_currentSprite];
            renderer.color = new Color(255, 255, 255, 255);
            yield return new WaitForSeconds(.75f);
        }
    }

    public bool TakeDamage()
    {
        m_healthPoints = System.Math.Max(m_healthPoints - 1, 0);

        if (m_healthPoints == 0)
        {
            Destroy(gameObject);
            m_player.GetComponent<Player>().AddPoint();
            OnCollect?.Invoke();

            return false;
        }

        return true;
    }
}
