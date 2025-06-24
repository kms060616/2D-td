using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireCooldown = 0.2f;
    private float fireTimer = 0f;

    public int maxAmmo = 6;
    private int currentAmmo;
    public float reloadTime = 1.5f;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0) && fireTimer >= fireCooldown && currentAmmo > 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        fireTimer = 0f;
        currentAmmo--;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<PlayerAttack>().Init(dir);

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("재장전 중...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("재장전 완료");
    }
}
