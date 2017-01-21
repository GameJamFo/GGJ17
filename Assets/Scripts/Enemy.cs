using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public float Speed = 5;

    private GameObject target;
    private Rigidbody rigid;
    private Animator anim;
    private NavMeshAgent agent;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    
    private float lastHit { get; set; }
    private bool canAttack {
        get {
            bool c = (lastHit + 2) < (Time.time); ;
            canAttack = false;
            return c;
        }
        set {
            if(value == false)
            {
                lastHit = Time.time;
            } else
            {
                // not sure why it should be set to true.
            }
        }
    }
	void Update () {

        var playerTarget = GameObject.FindWithTag("Player");
        RaycastHit hit;
        if (Physics.Linecast(transform.position, playerTarget.transform.position, out hit) && hit.collider.transform == playerTarget.transform)
        {
            agent.SetDestination(playerTarget.transform.position);
            target = playerTarget;
        } else
        {
            target = null;
        }


        if (target == null && agent.remainingDistance < 3f)
        {
            agent.SetDestination(transform.position + (Random.insideUnitSphere * 8));
        }

        anim.SetBool("walk", agent.velocity.sqrMagnitude > 0.2f);

        if (target && agent.remainingDistance < 2 && canAttack)
        {
            anim.SetTrigger("attack");
            target.GetComponent<PlayerManager>().Hit();
        }
    }

    public void Hit()
    {
        anim.SetTrigger("hit");
    }
}