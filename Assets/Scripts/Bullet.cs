using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody.velocity = transform.right * speed;    
    }

    // update is called once per frame
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "enemy_male")
        {
            EnemyMaleController enemymale = hitInfo.GetComponent<EnemyMaleController>();
            if (enemymale != null && enemymale.health > 0)
            {
                enemymale.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (hitInfo.tag == "enemy_female")
        {
            EnemyFemaleController enemyfemale = hitInfo.GetComponent<EnemyFemaleController>();
            if (enemyfemale != null && enemyfemale.health > 0)
            {
                enemyfemale.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
