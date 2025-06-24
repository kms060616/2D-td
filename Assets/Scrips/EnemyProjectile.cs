using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour, IEnemyProjectile
{
    public float speed = 4f;
    public float lifeTime = 4f;
    public int damage = 1;
    private Vector2 direction;

    public void Initialize(Vector2 dir)
    {
        direction = dir;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player p = other.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

}
