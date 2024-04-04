using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject player;
    private Rigidbody2D rb;
    private Animator anim; // not implemented yet
    private Transform currentPoint;


    // z tym wraca do patrolowania tam gdzie sko?czy?
    private Transform lastPointOfPatrol;
    private Transform lastTargetOfPatrol;

    public float baseSpeed;
    public float chaseSpeed;
    private float currentSpeed;

    private float distance;

    public bool isChasing;
    private bool isReturning;
    public float detectionRange;
    private float chasingRange;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        currentSpeed = baseSpeed;
        // anim.SetBool("isRunning", true);

        isChasing = false;
        isReturning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChasing)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < detectionRange)
            {
                isChasing = true;
                currentSpeed = chaseSpeed;
                lastPointOfPatrol = transform; // aby zacz?? od tego miejsca gdzie sko?czy?
                lastTargetOfPatrol = currentPoint; // aby szed? w t? sam? stron? w któr? szed?
                chasingRange = detectionRange+2;
                currentPoint = player.transform;
            }

            if (currentPoint == pointB.transform)
            {
                // TODO: ogarn?? Time.deltaTime
                // currentSpeed * Time.deltaTime
                rb.velocity = new Vector2(currentSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-currentSpeed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                currentPoint = pointB.transform;
            }
        } else
        {
            if (isReturning)
            {
                Vector2 direction = lastPointOfPatrol.transform.position - transform.position;
                // vector wskazuj?cy w kierunku ostatnio patrolowanego punktu
                if (direction.x > 0)
                {
                    // currentSpeed * Time.deltaTime
                    rb.velocity = new Vector2(currentSpeed, 0);
                }
                else
                {
                    rb.velocity = new Vector2(-currentSpeed, 0);
                }

                if(Vector2.Distance(transform.position, lastPointOfPatrol.transform.position) < 0.5f)
                {
                    isReturning = false;
                    isChasing = false;
                    currentSpeed = baseSpeed;
                    currentPoint = lastTargetOfPatrol;
                }

            } else
            {
                // TODO: zrobi? aby w ka?dym behaviourze wykorzysta? zmiennej? distance(poni?ej)
                distance = Vector2.Distance(transform.position, player.transform.position);
                if (distance > chasingRange)
                {
                    isReturning = true;
                }
                // TODO: zimplementowa? to
                // aby ?atwiej by?o zgubi? z czasem
                chasingRange -= 0.2f * Time.deltaTime; 

                Vector2 direction = player.transform.position - transform.position;
                // vector wskazuj?cy w kierunku gracza
                if (direction.x > 0)
                {
                    // currentSpeed * Time.deltaTime
                    rb.velocity = new Vector2(currentSpeed, 0);
                }
                else
                {
                    rb.velocity = new Vector2(-currentSpeed, 0);
                }

                // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, currentSpeed);



            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.10f);

        Gizmos.color = Color.green;
        if (!isChasing)
        {
            Gizmos.DrawWireSphere(pointA.transform.position, 0.4f);
            Gizmos.DrawWireSphere(pointB.transform.position, 0.4f); 
            Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);


            // DetectionRange
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.transform.position);

            // Lose interest for chasing range
            Gizmos.DrawWireSphere(transform.position, chasingRange);
            
        }

    }
}
