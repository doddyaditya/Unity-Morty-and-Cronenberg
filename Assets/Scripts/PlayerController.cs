using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Health player
    [SerializeField] public float health;
    //Mengatur kecepatan player bergerak
    [SerializeField] private float speed;
    //Komponen rigidbody 2D
    private Rigidbody2D rigidBody;
    //Player bergerak ke kanan atau ke kiri
    private float moveInput;
    //Arah player bergerak ke kanan
    private bool facingRight;
    //Memberikan nilai Player melompat
    [SerializeField] private float jumpForce;
    //Kondisi player menginjak tanah
    [SerializeField] private bool isGrounded;
    //Posisi kaki player berada di bawah
    [SerializeField] private Transform feetPos;
    //Radius kaki player
    [SerializeField] private float circleRadius;
    //Memastikan object yang digunakan sebagai ground
    [SerializeField] private LayerMask whatIsGround;
    //Menjalankan animasi idle, run, attack, jump
    private Animator anim;
    API highscoreManager;
    public GameObject inputForm;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public LayerMask whatIsEnemies2;
    public float attackRange;
    public AudioSource backgroundmusic;
    private float volume;

    // Start is called before the first frame update
    void Start()
    {
        //Inisialisasi komponen rigidbody2d pada player
        rigidBody = GetComponent<Rigidbody2D>();
        //Set player menghadap ke kanan
        facingRight = true;
        //Inisialisasi komponen animatorr pada player
        anim = GetComponent<Animator>();
        highscoreManager = GetComponent<API>();
        inputForm.SetActive(false);
        volume = PlayerPrefs.GetFloat("VolumeSetting");
        backgroundmusic.volume = volume;
    }

    private void Update()
    {
        //Dengan memanggil class Physics2D dan fungsi OverlapCircle
        //yang memiliki 3 parameter ini menandakan bahwa
        //isGrounded akan bernilai benar jika ketiga parameter tersebut terpenuhi
        isGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);
        if (health > 0)
        {
            //Fungsi untuk Player saat melompat
            CharacterJump();
        }
    }

    private void FixedUpdate()
    {
        if(health > 0)
        {
            // Fungsi yang memanage inputan saat Player bergerak ke Kanan atau ke Kiri
            CharacterMovement();
            // Fungsi yang mengatur transisi animasi Player saat idle, run
            CharacterAnimation();
            //Fungsi untuk Player saat melakukan attack melee
            CharacterAttack();
            //Fungsi untuk Player saat melakukan tembakan
            CharacterShoot();
        }    
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void CharacterMovement()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (moveInput > 0 && facingRight == false)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight == true)
        {
            Flip();
        }
        // nilai pada sumbu X akan bertambah sesuai dg speed * moveInput
        if(!anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Shoot"))
        {
            rigidBody.velocity = new Vector2(speed * moveInput, rigidBody.velocity.y);
        }
    }

    void CharacterJump()
    {

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("isJump");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    void CharacterAttack()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                anim.SetTrigger("isAttack");
                rigidBody.velocity = Vector2.zero;
                Collider2D[] enemiesMaleToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesMaleToDamage.Length; i++)
                {
                    enemiesMaleToDamage[i].GetComponent<EnemyMaleController>().TakeDamage(25);
                }
                Collider2D[] enemiesFemaleToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies2);
                for (int i = 0; i < enemiesFemaleToDamage.Length; i++)
                {
                    enemiesFemaleToDamage[i].GetComponent<EnemyFemaleController>().TakeDamage(25);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void CharacterShoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Shoot"))
        {
            anim.SetTrigger("isShoot");
            rigidBody.velocity = Vector2.zero;
        }
    }

    void CharacterAnimation()
    {
        if (moveInput != 0 && isGrounded == true)
        {
            anim.SetBool("isRun", true);
        }
        else if (moveInput == 0 && isGrounded == true)
        {
            anim.SetBool("isRun", false);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Die());
            inputForm.SetActive(true);
        }
    }

    public IEnumerator Die()
    {
        anim.SetTrigger("isDead");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
