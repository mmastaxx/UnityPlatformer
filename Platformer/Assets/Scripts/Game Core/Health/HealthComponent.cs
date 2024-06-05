using System;
using System.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth = 100.0f;

    [Header("iFrames")]
    [SerializeField] float iFrameDuration = 1.0f;
    [SerializeField] int numberOfFlash = 6;

    AudioManager audioManager;
    const int PLAYER_LAYER = 7;
    const int ENEMY_LAYER = 6;
    SpriteRenderer spriteRenderer;
    bool invulnerable = false;
    bool dead = false;
    float currentHealth;
    private HealthBar healthBar;
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
        if (!invulnerable && !dead)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

            healthBar.SetHealth(currentHealth);
            if (currentHealth > 0)
            {
                animator.SetTrigger("hurt");
                StartCoroutine(Invulnerability());
                if (gameObject.layer == ENEMY_LAYER)
                {
                    if (gameObject.tag == "Boss")
                    {
                        audioManager.Play("hurt" + UnityEngine.Random.Range(1, 3));
                    }
                    else
                    {
                        audioManager.Play("hurtEnemy" + UnityEngine.Random.Range(1, 4));
                    }
                }
                else if (gameObject.layer == PLAYER_LAYER)
                {
                    audioManager.Play("hurt" + UnityEngine.Random.Range(1, 3));
                }

            }
            else
            {
                dead = true;
                animator.SetBool("bAlive", false);
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
    private void Death(GameObject obj)
    {
        if (gameObject.layer == PLAYER_LAYER)
            PlayerDeath();
        else
        {
            if (gameObject.tag == "Boss")
            {
                Destroy(gameObject);
                BossDeath();
            }
            else
                EnemyDeath();
        }
    }
    private void PlayerDeath()
    {
        EventManager.OnPlayerDead(gameObject);
        audioManager.Play("death");
        gameObject.GetComponent<Movement>().enabled = false;
        gameObject.GetComponent<CombatComponent>().enabled = false;
    }
    private void EnemyDeath()
    {
        EventManager.OnEnemyDead(gameObject);
        audioManager.Play("deathEnemy");
        EnemyMovement movement = gameObject.GetComponent<EnemyMovement>();
        EnemyCombat enemyCombat = gameObject.GetComponent<EnemyCombat>();
        movement.enabled = false; 
         enemyCombat.enabled = false; 
    }
    private void BossDeath()
    {
        audioManager.Play("death");
        EventManager.OnGameEnd();
    }
}
