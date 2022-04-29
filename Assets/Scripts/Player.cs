using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    private Vector3 playerScale;
    public float playerSpeed = 1;
    private int m_healthPoints = 10;
    private int m_points = 0;
    private int m_maxPoints = 0;
    
    public Animator animator;

    public GameObject CrystalsCountText;
    protected Dictionary<Collider2D, Coroutine> m_damageCoroutines = new Dictionary<Collider2D, Coroutine>();

    private void Start()
    {
        playerScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        boxCollider = GetComponent<BoxCollider2D>();
        m_maxPoints = GameObject.FindGameObjectsWithTag("Collectible").Length;
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            animator.SetBool("Movement", true);
        }
        else animator.SetBool("Movement", false);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //reset moveDelta

        moveDelta = new Vector3(x, y, 0);

        //sprite direction and adding speed

        if (moveDelta.x > 0)
        {
            transform.localScale = playerScale;
            moveDelta.x += playerSpeed;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-playerScale.x, playerScale.y, playerScale.z);
            moveDelta.x -= playerSpeed;
        }

        if (moveDelta.y > 0)
        {
            moveDelta.y += playerSpeed;
        }
        else if (moveDelta.y < 0)
        {
            moveDelta.y -= playerSpeed;
        }

        if ((moveDelta.y != 0 || moveDelta.x != 0) && GetComponent<AudioSource>().isPlaying == false)
        {
            GetComponent<AudioSource>().Play();
            //animator.SetBool("Movement", true);
        }


        //colisioion detection and moving
        Vector3 positionWithOffset = new Vector3(transform.position.x + (boxCollider.offset.x * playerScale.x), transform.position.y + (boxCollider.offset.y * playerScale.y), transform.position.z);
        //X

        hit = Physics2D.BoxCast(positionWithOffset, boxCollider.size * playerScale.x, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

        // z

        hit = Physics2D.BoxCast(positionWithOffset, boxCollider.size * playerScale.y, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Actor") && !m_damageCoroutines.ContainsKey(collider))
        {
            m_damageCoroutines.Add(collider, StartCoroutine(nameof(TakeDamageCoroutine)));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Actor") && m_damageCoroutines.ContainsKey(collider))
        {
            StopCoroutine(m_damageCoroutines[collider]);
            m_damageCoroutines.Remove(collider);
        }
    }

    public IEnumerator TakeDamageCoroutine()
    {
        var renderer = GetComponent<SpriteRenderer>();
        while (TakeDamage())
        {
            renderer.color = new Color(255, 0, 0, 255);
            yield return new WaitForSeconds(.1f);
            renderer.color = new Color(255, 255, 255, 255);
            yield return new WaitForSeconds(.9f);
        }
    }

    public bool TakeDamage()
    {
        m_healthPoints = System.Math.Max(m_healthPoints - 1, 0);

        if (m_healthPoints == 0)
        {
            // end game

            return false;
        }

        return true;
    }

    public void AddPoint()
    {
        m_points++;

        CrystalsCountText.GetComponent<CrystalsController>().UpdateCollectiblesText();

        if (m_points >= m_maxPoints)
        {
            // finish game
        }
    }
}
