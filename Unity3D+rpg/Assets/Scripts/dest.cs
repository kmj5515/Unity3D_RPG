using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class dest : MonoBehaviour
{
    private NavMeshAgent agent;
    GameObject player;
    public float dist;

    public bool isTraceNevi;
    // Start is called before the first frame update
    void Start()
    {
        //player = FindObjectOfType<GameObject>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("Player Not Found");
        }
        else
        {
            Debug.LogWarning("Player Found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, player.transform.position);
        if (isTraceNevi)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }
}
