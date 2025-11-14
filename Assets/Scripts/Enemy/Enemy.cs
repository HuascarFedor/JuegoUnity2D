using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // public float speed = 7;
    public Transform targetTransform;
    private Rigidbody2D rb2d;
    NavMeshAgent navMeshAgent;
    Animator animator;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    void Update()
    {
        /*
        rb2d.MovePosition(
            Vector2.MoveTowards(
                transform.position,
                targetTransform.position,
                speed * Time.deltaTime
            )
        );
        */
        navMeshAgent.SetDestination(
            targetTransform.position
        );

        AdjustAnimationAndRotation();
    }

    private void AdjustAnimationAndRotation()
    {
        bool isMoving = navMeshAgent.velocity.sqrMagnitude > 0.01f;
        animator.SetBool("isRunning", isMoving);

        if (navMeshAgent.desiredVelocity.x > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        
        if (navMeshAgent.desiredVelocity.x < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
