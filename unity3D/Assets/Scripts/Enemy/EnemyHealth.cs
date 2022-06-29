using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


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

    public void init() //몬스터 재생성시 초기화 함수
    {
        enemyCurrentHealth = enemyStartHealth; // 체력이 가득찬 상태로 시작
        // 죽지않았고, 데미지를 받지 않았고, 가라앉지 않았다고 플래그 설정.
        isDead = false;
        damaged = false;
        isSinking = false;

        BoxCollider collider = transform.GetComponentInChildren<BoxCollider>();
        collider.isTrigger = false;

        GetComponent<NavMeshAgent>().enabled = true;
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

        // 죽지 않을때만 받음.
        if (!isDead)
        {
            try
            {
                TakeDamage(damage);
                PushBack(playerPosition,pushBack);

            }
            catch (MissingReferenceException e)
            {
                Debug.Log(e.ToString());
            }
        }
    }
    
    // 뒤로 밀려나는 함수, 플레이어의 위치와 밀려나는 정도를 매개 변수로 전달
    void PushBack(Vector3 playerPosition, float pushBack)
    {
        // 플레이어의 위치와 몬스터 위치의 차이를 벡터로 구한다.
        Vector3 diff = playerPosition - transform.position;

        // 플레이어와 몬스터 사이의 차이를 정규화 시킴(거리를 1로 만듦)
        diff = diff / diff.sqrMagnitude;

        // 현재 몬스터의 RigidBody에 힘을 가한다.
        // 플레이어 반대 방향으로 밀려나는데, pushBack만큼 비례해서 더 밀려난다.
        GetComponent<Rigidbody>().AddForce(diff*-10000f*pushBack);
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
        // 죽었음을 체크
        isDead = true;

        // 점수를 추가
        StageController.instance.AddPoint(10);

        // Trigger가 몬스터의 Collider를 true가 되도록 변경
        // Trigger가 true면 지면이나 플레이어와 충돌하지않음.
        BoxCollider collider = transform.GetComponentInChildren<BoxCollider>();
        collider.isTrigger = true;
        
        // 플레이어를 찾아 다니지 않도록 NavMeshAgent를 비활성화
        GetComponent<NavMeshAgent>().enabled = false;

        //가라앉도록함.
        isSinking = true;
        
    }

    public void StartSinking() // 적이 죽을시 적의 설정을 변경하고 게임오브젝트를 삭제한다.
    {
        GetComponent<NavMeshAgent>().enabled = false;

        GetComponent<Rigidbody>().isKinematic = true;

        isSinking = true;

        Destroy(gameObject, 2f);
    }
}
