using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    NavMeshAgent myAgent;
    Animator myAnim;
    public GameObject objetive;

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
        myAgent.speed = Random.Range(1.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        myAgent.destination = objetive.transform.position;
        if(myAgent.speed > 0)
        {
            myAnim.SetBool("walking", true);
        }
        else
        {
            myAnim.SetBool("walking", false);
        }
    }
}
