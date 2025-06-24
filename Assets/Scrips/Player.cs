using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Sprites")]
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private Sprite spriteDown;
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;

    [Header("HP")]
    public int maxHP = 5;
    private int currentHP;
    public int CurrentHP => currentHP;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Vector2 lastDirection = Vector2.down; // 기본 방향

    // Components
    private Rigidbody2D rb;
    private SpriteRenderer sR;
    private Animator animator;

    private Vector2 input;
    private Vector2 velocity;
    public GameObject poisonIcon; // Inspector에 PoisonIcon Image 드래그

    private Coroutine poisonCoroutine;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
    }

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        HandleInput();
        HandleAnimation();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            FireBullet();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void HandleInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        velocity = input.normalized * moveSpeed;

        if (input.sqrMagnitude > 0.01f)
        {
            lastDirection = input.normalized;
        }
    }

    private void HandleAnimation()
    {
        if (input.sqrMagnitude > 0.01f)
        {
            animator.SetFloat("MoveX", input.x);
            animator.SetFloat("MoveY", input.y);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void FireBullet()
    {
        if (lastDirection == Vector2.zero) lastDirection = Vector2.down;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<PlayerAttack>().Init(lastDirection);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"플레이어 피격! 현재 체력: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("플레이어 사망!");
        SeedInventory.Instance.SaveInventory();
        SceneManager.LoadScene("SampleScene");
    }

    public void ApplyPoison(float duration, float tickInterval, int damage)
    {
        if (poisonCoroutine != null)
            StopCoroutine(poisonCoroutine);

        poisonCoroutine = StartCoroutine(ApplyPoisonCoroutine(duration, tickInterval, damage));
    }

    IEnumerator ApplyPoisonCoroutine(float duration, float tickInterval, int damage)
    {
        poisonIcon.SetActive(true);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }

        poisonIcon.SetActive(false);
        poisonCoroutine = null;
    }


}
