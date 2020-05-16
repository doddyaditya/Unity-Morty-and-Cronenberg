using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{

    // Array of waypoints to walk from one to the next one
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform enemyPos;

    // Walk speed that can be set in Inspector
    [SerializeField] private float moveSpeed;

    // Index of current waypoint from which Enemy walks to the next one
    private int waypointIndex = 0;
    //Arah player bergerak ke kanan
    private bool facingRight;
    //Menjalankan animasi idle, run, attack, jump
    private Animator anim;

    // Use this for initialization
    private void Start()
    {
        // Set position of Enemy as position of the first waypoint
        transform.position = enemyPos.position;
        facingRight = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Move Enemy
        Move();
    }

    private void Move()
    {
        if (waypoints[waypointIndex].transform.position.x > transform.position.x && facingRight == false)
        {
            Flip();
        }
        else if (waypoints[waypointIndex].transform.position.x < transform.position.x && facingRight == true)
        {
            Flip();
        }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            anim.SetBool("isWalk", true);
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
        }
        if (transform.position.x == waypoints[waypointIndex].transform.position.x)
        {
            if (waypointIndex == 0)
            {
                waypointIndex += 1;
            }
            else if (waypointIndex == 1)
            {
                waypointIndex -= 1;
            }

        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}