using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    public float maxHealth { get; private set; } = 100.0f;
    public float currentHealth { get; private set; }
    Animator animator;
    [Header("iFrames")]
    [SerializeField] private float iFrameDuration = 1.0f;
    [SerializeField] private int numberOfFlash = 6;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(11.5f);
        }
    }
    void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log("Current Health: " + currentHealth);
        if (currentHealth > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            Debug.Log("Death?");
            animator.SetTrigger("death");
        }
    }
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(7, 6, true);
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(iFrameDuration * 0.75f);
        for (int i = 0; i < numberOfFlash; i++)
        {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration*0.25f / (numberOfFlash * 2));
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlash * 2));
        }
        spriteRenderer.color = Color.white;
        Physics2D.IgnoreLayerCollision(7, 6, false);
    }

}
