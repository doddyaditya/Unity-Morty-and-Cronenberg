using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaleController : MonoBehaviour
{

    [SerializeField] public float health;
    [SerializeField] private float attackDamage;
    [SerializeField] private GameObject deathEffect;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <=0)
        {
            ScoreScript.scoreValue += 30;
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        anim.SetTrigger("isDead");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Player")
        {
            PlayerController player = hitInfo.GetComponent<PlayerController>();
            if (player != null && player.health > 0 && health > 0 && !Input.GetKeyDown(KeyCode.Mouse1))
            {
                anim.SetTrigger("isAttack");
                player.TakeDamage(attackDamage);
            }
        }
    }
}
