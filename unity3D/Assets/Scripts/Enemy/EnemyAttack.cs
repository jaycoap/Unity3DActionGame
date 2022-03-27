using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInrange;
    float timer;

    void Awake() // 변수들을 초기화
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void OnTriggerEnter(Collider other) //Sphere Collider에 범위에 player가 들어온다면 공격 범위 작동
    {
        if(other.gameObject == player)
        {
            playerInrange = true;
        }
    }

    void OnTriggerExit(Collider other) //Sphere Collider에 player가 나간다면 공격 범위 중지
    {
        if(other == player)
        {
            playerInrange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInrange && enemyHealth.enemyCurrentHealth > 0)
        {
            Attack();
        }
    }

    void Attack() // 적이 공격시 PlayerHealth.cs에 있는 TakeDamage를 불러옴.
    {
        timer = 0f;
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

}
