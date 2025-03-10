using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]

public class Enemies : MonoBehaviour
{   [Header("Elements")]
    private Player player;

    [Header("Component")]
    private EnemyMovement movement;

    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer spriteRenderer; // Renamed from renderer
    [SerializeField] private SpriteRenderer spawnIndicator;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;

    [Header ( "Attack ")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    [SerializeField] private float playerDetectionRadius;
    private float attackDelay;
    private float attackTimer;
    private bool hasSpawned;

    [Header("DEBUG")]
    [SerializeField] private bool gizmos;

    private Vector3 targetScale;
    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        player = FindFirstObjectByType<Player>();
        if (player == null)
        {
            Debug.LogWarning("Cannot find Player... Dude walks away");
            Destroy(gameObject);
        }
        StartSpawnSequence();
        attackDelay = 1f / attackFrequency;
    }

    private void StartSpawnSequence()
    {
        SetRenderersVisibility(false);
        targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, 3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);


    }

    private void SetRenderersVisibility(bool visibility = true)
    {
        spriteRenderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }

    private void SpawnSequenceCompleted()
    {
        SetRenderersVisibility();
        hasSpawned = true;
        movement.StorePlayer(player);
    }
    // Update is called once per frame
    void Update()
    {
        if(attackTimer >= attackDelay)
           TryAttack();
            else
            Wait();
    }
    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }
    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= playerDetectionRadius)
            Attack();
    }
    private void Attack()
    {
        attackTimer = 0;
        player.TakeDamage(damage);
    }

    private void PassAway()
    {
        passAwayParticles.transform.SetParent(null);
        passAwayParticles.Play();
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }

}
