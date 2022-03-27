using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int enemyStartHealth = 100; // 적 시작 체력 설정.
    public int enemyCurrentHealth; // 적 현재 체력

    public float flashSpeed = 5f; // 적 피격시 테두리 변화 속도
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f); // 적 피격시 테두리 변화 색

    public float sinkSpeed = 1f;

    // 적의 상태를 구분하기 위한 것.
    bool isDead;
    bool isSinking;
    bool damaged;

    void Awake()
    {
        enemyCurrentHealth = enemyStartHealth;
    }

    public void TakeDamage (int amount) // 캐릭터가 적에게 공격이 적중되었을때 체력 감소 함수
    {
        damaged = true;

        enemyCurrentHealth -= amount;

        if(enemyCurrentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public IEnumerator StartDamage(int damage, Vector3 playerPosition, float delay, float pushBack) // 캐릭터가 적에게 공격이 적중되었을때 튕겨져 나가는 효과 표현 함수.
    {
        yield return new WaitForSeconds(delay);

        try
        {
            TakeDamage(damage);
            Vector3 diff = playerPosition - transform.position;
            diff = diff / diff.sqrMagnitude;
            GetComponent<Rigidbody>().AddForce((transform.position - new Vector3(diff.x, diff.y, 0f)) * 50f * pushBack);

        }catch(MissingReferenceException e)
        {
            Debug.Log(e.ToString());
        }
    }

    void Update()
    {
        if (damaged)
        {
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", flashColour);
        }
        else
        {
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor",
                Color.Lerp(transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_OutlineColor"), Color.black,flashSpeed * Time.deltaTime));
        }
        damaged=false;

        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    void Death() // 적이 죽었을시 적의 collider를 True로 하여 땅바닥을 뚫고 아래로 가라앉도록 한다.
    {
        isDead = true;

        transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;
        StartSinking();
    }

    public void StartSinking() // 적이 죽을시 적의 설정을 변경하고 게임오브젝트를 삭제한다.
    {
        GetComponent<NavMeshAgent>().enabled = false;

        GetComponent<Rigidbody>().isKinematic = true;

        isSinking = true;

        Destroy(gameObject, 2f);
    }
}
