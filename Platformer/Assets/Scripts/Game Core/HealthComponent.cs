using System;
using System.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float maxHealth = 100.0f;
    
    [Header("iFrames")]
    [SerializeField] float iFrameDuration = 1.0f;
    [SerializeField] int numberOfFlash = 6;

    SpriteRenderer spriteRenderer;
    bool invulnerable;
    float currentHealth;
    public HealthBar healthBar;
    Animator animator;
    public event Action<GameObject> OnDead;
    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }
    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        if (!invulnerable)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

            healthBar.SetHealth(currentHealth);
            if (currentHealth > 0)
            {
                animator.SetTrigger("hurt");
                StartCoroutine(Invulnerability());
            }
            else
            {
                animator.SetTrigger("death");
            }
        }
    }
    public void IFramesOn()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(7, 6, true);
    }
    public void IFramesOff()
    {
        invulnerable = false;
        Physics2D.IgnoreLayerCollision(7, 6, false);
    }
    private IEnumerator Invulnerability()
    {
        IFramesOn();
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(iFrameDuration * 0.75f);
        if (numberOfFlash > 0)
        {
            for (int i = 0; i < numberOfFlash; i++)
            {
                spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(iFrameDuration * 0.25f / (numberOfFlash * 2));
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                yield return new WaitForSeconds(iFrameDuration / (numberOfFlash * 2));
            }
        }
        else
            yield return new WaitForSeconds(iFrameDuration * 0.25f);
        spriteRenderer.color = Color.white;
        IFramesOff();
    }
    public void Death(GameObject obj) 
    {
        Debug.Log("Death");
        OnDead?.Invoke(gameObject);
    }
}
