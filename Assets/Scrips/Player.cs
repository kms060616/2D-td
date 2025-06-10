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
    public Transform firePoint; // �Ѿ� ������ ��ġ
    private Vector2 lastDirection = Vector2.down; // ������ �ٶ� ����

    public int maxHP = 5;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"�÷��̾� �ǰ�! ���� ü��: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("�÷��̾� ���!");
        SeedInventory.Instance.SaveInventory(); // �����ϰ�
        SceneManager.LoadScene("SampleScene");  // ���� ������ ����
        // ���� ���� ó�� or �����
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
