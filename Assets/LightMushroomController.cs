using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMushroomController : MonoBehaviour
{
    protected Dictionary<Collider2D, Coroutine> m_damageCoroutines = new Dictionary<Collider2D, Coroutine>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Actor")) {
            m_damageCoroutines.Add(collider, StartCoroutine(nameof(TakeDamage), collider));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Actor"))
        {
            StopCoroutine(m_damageCoroutines[collider]);
            m_damageCoroutines.Remove(collider);
        }
    }

    public IEnumerator TakeDamage(object value)
    {
        var collider = (Collider2D)value;
        var controller = collider.GetComponent<EnemyController>();
        var renderer = collider.GetComponent<SpriteRenderer>();
        while (controller.TakeDamage())
        {
            renderer.color = new Color(255, 0, 0, 255);
            yield return new WaitForSeconds(.1f);
            renderer.color = new Color(255, 255, 255, 255);
            yield return new WaitForSeconds(.9f);
        }
    }
}
