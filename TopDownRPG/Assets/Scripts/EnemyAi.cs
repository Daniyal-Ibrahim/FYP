using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    /*
     * Finish FSM-Based AI to unlock Behaviour Tree AI
     */
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator animator;

    public bool isDead;
    public bool isRanged;
    public float AIPositionX, AIPositionY, AIPositionZ;


    //Patroling
    public Vector3 walkPoint;
    [SerializeField] bool walkPointSet;
    bool waiting;
    public float walkPointRange;
    public GameObject walkpointObj;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public GameObject spawnPoint;

    //Attack 2.0
    private int AID = 0;
    List<string> Attacks = new List<string>(new string[] { "Attack1", "Attack2", "Attack3" });
    public bool isAttacking = false;
    public float waitTime = 1.5f;
    public float timer = 0f;

    //States
    public float sightRange, attackRange, rangedAttack,agroRange;
    public bool playerInSightRange, playerInAttackRange, playerInRangedAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        //SearchWalkPoint();
    }
    public AdvancedAudioManager audioManager;
    public void FootStep()
    {
        audioManager = GameObject.Find("Advanced Audio Manager").GetComponent<AdvancedAudioManager>();
        audioManager.PlaySound("FootStep");
    }

    private void Update()
    {
        AIPositionX = transform.position.x;
        AIPositionY = transform.position.y;
        AIPositionZ = transform.position.z;

        if (!isDead && Physics.CheckSphere(transform.position, agroRange, whatIsPlayer))
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            playerInRangedAttackRange = Physics.CheckSphere(transform.position, rangedAttack, whatIsPlayer);
            // position for save files

            // basic states 
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
            if (isRanged)
            {
                if (playerInRangedAttackRange && playerInSightRange && !playerInAttackRange) RangedAttack();
                if (playerInSightRange && !playerInRangedAttackRange) ChasePlayer();
                if (playerInRangedAttackRange && playerInSightRange && playerInAttackRange) RePosition();
            }
            timer += Time.deltaTime;
        }
    
    }

    private void RePosition()
    {
       //walkPoint = new Vector3(walkpointObj.transform.position.x, walkpointObj.transform.position.y, walkpointObj.transform.position.z);
        
        Patroling();
    }

    private void Patroling()
    {

        if (!walkPointSet && !waiting) SearchWalkPoint();

        if (walkPointSet)
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Attacking", false);
            agent.SetDestination(walkPoint);
            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
            {
                animator.SetBool("Walking", false);
                walkPointSet = false;
                waiting = true;
                Invoke(nameof(SearchWalkPoint), 2f);
                Debug.Log("Waiting");
            }
        }


    }

    private void Patroling(Vector3[] vectors)
    {
        for (int i = 0; i < vectors.Length;)
        {
            agent.SetDestination(vectors[i]);
            animator.SetBool("Walking", true);
            animator.SetBool("Attacking", false);
            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                animator.SetBool("Walking", false);
                walkPointSet = false;
                i++;
                if (i == vectors.Length) { i = 0; }
            }
        }
    }

    private void SearchWalkPoint()
    {
        waiting = false;
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            animator.SetBool("Walking", true);
            animator.SetBool("Attacking", false);
        }
        else
        {
            animator.SetBool("Walking", false);
            walkPointSet = false;
        }

        // add code to re-get walkpoint if walkpoint not reachable 
    }

    private void ChasePlayer()
    {
        animator.SetBool("Walking", true);
        agent.SetDestination(player.position);
        // add states to allow running
        // or more aggressive behaviour
    }
    bool fired = false;

    public void SpawnProjectile()
    {
        GameObject bullet = Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
    }

    private void RangedAttack()
    {
        transform.LookAt(player);
        agent.SetDestination(transform.position);
        animator.SetBool("Walking", false);
        animator.SetFloat("AttackID", 4.0f);
        Debug.Log("Fired :" + fired);
        if (timer < waitTime)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", true);          
        }
        else
        {
            animator.SetBool("Attacking", false);
            AID = 0;
            timer = 0;
            animator.SetFloat("AttackID", 0);
        }
    }
    private void AttackPlayer()
    {
        transform.LookAt(player);
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);


        //Debug.Log("Timer :" + timer);
        if (timer > waitTime)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", true);
            if (AID != 3)
            {
                AID++;
                animator.SetTrigger(Attacks[AID]);
                waitTime -= .5f;
            }
            else
            {
                AID = 0;
                waitTime = 2f;
            }


            timer = 0;
        }
        else
        {
            animator.SetBool("Attacking", false);
        }


        if (!alreadyAttacked)
        {
        //    ///Attack code here
        //    animator.SetBool("Walking", false);
        //    animator.SetBool("Attacking", true);
        //    ///End of attack code

        //    if (AID != 3)
        //    {
        //        AID++;
        //    }
        //    else
        //    {
        //        AID = 0;
        //    }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        animator.SetBool("Attacking", false);
        alreadyAttacked = false;
        //StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSecondsRealtime(2f);
            alreadyAttacked = false;
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, agroRange);
    }
}
