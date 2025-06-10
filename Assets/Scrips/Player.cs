using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float moveSpeed = 2f;

    [SerializeField] Sprite spriteUp;
    [SerializeField] Sprite spriteDown;
    [SerializeField] Sprite spriteLeft;
    [SerializeField] Sprite spriteRight;

    Animator animator;

    Rigidbody2D rb;
    SpriteRenderer sR;

    Vector2 input;
    Vector2 velocity;

    public int CurrentHP => currentHP;

    public GameObject bulletPrefab;
    public Transform firePoint; // 총알 나오는 위치
    private Vector2 lastDirection = Vector2.down; // 마지막 바라본 방향

    public int maxHP = 5;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
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

    void Die()
    {
        Debug.Log("플레이어 사망!");
        SeedInventory.Instance.SaveInventory(); // 저장하고
        SceneManager.LoadScene("SampleScene");  // 시작 씬으로 복귀
        // 게임 오버 처리 or 재시작
        // SceneManager.LoadScene("GameOverScene");
    }





    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        velocity = input.normalized * moveSpeed;



        if (input.sqrMagnitude > 0.01f)
        {


            animator.SetFloat("MoveX", input.x);
            animator.SetFloat("MoveY", input.y);
            animator.SetBool("IsMoving", true);
            lastDirection = input.normalized;


        }
        else
        {
            animator.SetBool("IsMoving", false);
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            FireBullet();
        }

        void FireBullet()
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<PlayerAttack>().direction = lastDirection;
        }

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    
}
