using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float speed = 8f;
    public Vector2 direction;
    public float lifeTime = 0.5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
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
