using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEnemy : MonoBehaviour
{
    public int baseHP = 2;
    private int currentHP;

    private RoomController room;

    public GameObject projectilePrefab;
    public float attackCooldown = 2f;
    private float attackTimer = 0f;
    public float attackRange = 5f;
    private Transform player;

    public GameObject seedPrefab; // ¾¾¾Ñ ÇÁ¸®ÆÕ (°øÅë)
    public string seedType;       // Á¾·ù ¿¹: "¼±ÀÎÀå", "¹ö¼¸", "²É"

    void Start()
    {
        room = GetComponentInParent<RoomController>();
        if (room != null && !room.plantEnemies.Contains(this))
        {
            room.plantEnemies.Add(this);
        }

        // fallback: ÃÖ¼Ò Ã¼·Â º¸Àå
        currentHP = baseHP;
    }

    public void ApplyLevelScaling(int level)
    {
        currentHP = baseHP + level - 1;
        Debug.Log($"{gameObject.name} Ã¼·Â: {currentHP}");
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        room.OnPlantDied(this);

        // ¾¾¾Ñ »ý¼º
        if (seedPrefab != null)
        {
            GameObject seed = Instantiate(seedPrefab, transform.position, Quaternion.identity);
            SeedItem seedItem = seed.GetComponent<SeedItem>();
            if (seedItem != null)
            {
                seedItem.seedType = seedType;
            }
        }

        Destroy(gameObject);
    }

    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null) return;

        attackTimer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange && attackTimer >= attackCooldown)
        {
            ShootAtPlayer();
            attackTimer = 0f;
        }
    }

    void ShootAtPlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<EnemyProjectile>().Initialize(dir);
    }
}
