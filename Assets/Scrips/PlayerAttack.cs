using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 0.5f;

    private Vector2 moveDirection;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    public void Init(Vector2 direction)
    {
        moveDirection = direction.normalized;

        // 총알 회전 방향 설정 (2D용)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlantEnemy>(out PlantEnemy enemy))
        {
            enemy.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
