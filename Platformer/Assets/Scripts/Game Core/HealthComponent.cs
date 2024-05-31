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

    AudioManager audioManager;
    const int PLAYER_LAYER = 7;
    const int ENEMY_LAYER = 6;
    SpriteRenderer spriteRenderer;
    bool invulnerable;
    float currentHealth;
    public HealthBar healthBar;
    Animator animator;
    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        audioManager = FindObjectOfType<AudioManager>();
    }
    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        if (!invulnerable)
        {
            if (currentHealth > 0)
            {
                currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

                healthBar.SetHealth(currentHealth);
                if (gameObject.layer == ENEMY_LAYER)
                {
                    audioManager.Play("hurtEnemy" + UnityEngine.Random.Range(1, 4));
                } else if (gameObject.layer == PLAYER_LAYER)
                {
                    audioManager.Play("hurt" + UnityEngine.Random.Range(1, 3));
                }
                animator.SetTrigger("hurt");
                StartCoroutine(Invulnerability());
            }
            else
            {
                animator.SetTrigger("death");
                if (gameObject.layer == ENEMY_LAYER)
                {
                    audioManager.Play("deathEnemy");
                }
                else if (gameObject.layer == PLAYER_LAYER)
                {
                    audioManager.Play("death");
                }
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
    private void Death(GameObject obj) 
    {
        if (gameObject.layer == PLAYER_LAYER) { EventManager.OnPlayerDead(gameObject);} else { EventManager.OnEnemyDead(gameObject); }
        Debug.Log($"Death of: {gameObject.name}");
    }
}
