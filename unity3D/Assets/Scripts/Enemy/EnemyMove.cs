using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMove : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // player라는 태그를 가진 오브젝트의 위치를 가져옴.
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (nav.enabled)
        {
            nav.SetDestination(player.position);
        }
    }
}
