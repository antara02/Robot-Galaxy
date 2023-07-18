using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private Transform follow;
    [SerializeField] private float attackRadius;
    [SerializeField] private float followRadius;
    [SerializeField] private float randomPatrolRange;
    [SerializeField] private float minDistance;
    [SerializeField] private float shootRange;
    [Range(0, 1)] [SerializeField] private float turnSpeed;
    [SerializeField] private float shootCooldownTime;
    bool isRandomTargetReached;
    Vector3 targetPosition;

    bool canShoot;
    private NavMeshAgent nav;
    bool hasPatrolStarted;
    Vector3 patrolCenter;

    // Health
    public float health = 50f;
    public float damage = 10f;

    private Animator Anim;
    private bool dead = false;
    public GameObject muzzleFlesh;
    public Transform pos;

    void Awake()
    {
        isRandomTargetReached = true;
        nav = GetComponent<NavMeshAgent>();
        canShoot = true;
        hasPatrolStarted = false;
        patrolCenter = transform.position;
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead){
            if (Vector3.Distance(transform.position, follow.position) >= followRadius)
            {
                // Patrol
                if(hasPatrolStarted == false)
                {
                    patrolCenter = transform.position;
                    hasPatrolStarted = true;
                }

                if (isRandomTargetReached)
                {
                    targetPosition = new Vector3(patrolCenter.x + Random.Range(-randomPatrolRange, randomPatrolRange), patrolCenter.y, patrolCenter.z + Random.Range(-randomPatrolRange, randomPatrolRange));
                    isRandomTargetReached = false;

                }
                else
                {
                    if (Vector3.Distance(transform.position, targetPosition) < minDistance)
                    {
                        isRandomTargetReached = true;
                    }
                }

            }
            else if (Vector3.Distance(transform.position, follow.position) >= attackRadius)
            {
                hasPatrolStarted = false;
                targetPosition = follow.position;
            }
            else
            {
                hasPatrolStarted = false;
                targetPosition = transform.position;
                if (canShoot && !dead)
                {
                    canShoot = false;
                    Shoot();
                    Invoke("StopShootCooldown", shootCooldownTime);

                }

            }

            nav.SetDestination(targetPosition);
            Anim.SetBool("IsRunning", true);

            if(Vector3.Distance(transform.position, follow.position) <= attackRadius)
            {
                //Anim.SetBool("IsRunning", false);
            }
            
            Vector3 lookDir = follow.position - transform.position;

            transform.forward = new Vector3(lookDir.x, 0, lookDir.z).normalized;
        }
    }

    void StopShootCooldown()
    {
        canShoot = true;
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0,1,0), transform.forward, out hit, shootRange))
        {
            Debug.Log(hit.transform.name);
            Anim.SetBool("IsRunning", false);
            Anim.SetBool("Shooting", true);

            PlayerHealth player = hit.collider.GetComponent<PlayerHealth>();
            if(player != null){
                player.TakeDamage(damage);
                GameObject temp = Instantiate(muzzleFlesh, pos.position, Quaternion.identity);
                Destroy(temp, 0.5f);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            dead = true;
            Debug.Log("ea");
            Die();
        }
    }

    public void Die()
    {
        if(Anim.GetBool("IsRunning") == true)
        {
            Debug.Log("ea2");
            Anim.SetTrigger("Ded");
        }
        else
        {
            Debug.Log("ea2");
            Anim.SetTrigger("Ded");
        }

        Destroy(this.gameObject, 4f);
    }

}
