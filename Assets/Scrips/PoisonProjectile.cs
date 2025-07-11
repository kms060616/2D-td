using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonProjectile : MonoBehaviour, IEnemyProjectile
{
    public float speed = 4f;
    public float lifeTime = 4f;
    public int poisonDamage = 1;
    public float poisonDuration = 2f;
    public float poisonTickInterval = 0.5f;

    private Vector2 direction;

    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.ApplyPoison(poisonDuration, poisonTickInterval, poisonDamage);
            }

            Destroy(gameObject);
        }
    }

    IEnumerator ApplyPoison(Player player)
    {
        float elapsed = 0f;
        while (elapsed < poisonDuration)
        {
            player.TakeDamage(poisonDamage);
            yield return new WaitForSeconds(poisonTickInterval);
            elapsed += poisonTickInterval;
        }
    }
}
