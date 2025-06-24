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

    public GameObject seedPrefab; // 씨앗 프리팹 (공통)
    public string seedType;       // 종류 예: "선인장", "버섯", "꽃"

    void Start()
    {
        room = GetComponentInParent<RoomController>();
        if (room != null && !room.plantEnemies.Contains(this))
        {
            room.plantEnemies.Add(this);
        }

        // fallback: 최소 체력 보장
        currentHP = baseHP;
    }

    public void ApplyLevelScaling(int level)
    {
        currentHP = baseHP + level - 1;
        Debug.Log($"{gameObject.name} 체력: {currentHP}");
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

        // 씨앗 생성
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
        if (projectilePrefab == null)
        {
            Debug.LogError($"{gameObject.name}의 projectilePrefab이 비어 있습니다!");
            return;
        }

        Vector2 dir = (player.position - transform.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        var projectile = proj.GetComponent<IEnemyProjectile>();
        if (projectile != null)
        {
            projectile.Initialize(dir);
        }
        else
        {
            Debug.LogError($"{proj.name}에 IEnemyProjectile 인터페이스가 구현된 스크립트가 없습니다!");
        }
    }
}
