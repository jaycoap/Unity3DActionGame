using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]//이 코드는 게임 오브젝트에 Animator가 없으면 실행 할 수 없다는것.
public class PlayerMovement : MonoBehaviour
{

    protected Animator avatar; // 게임 오브젝트에 붙어있는 Animator 컴포넌트를 가져온다.
    protected PlayerAttack playerAttack;

    // lastAttackTime: 마지막으로 공격을 누른시점
    // lastSkillTime: 마지막으로 스킬을 누른 시점
    // lastDashTime: 마지막으로 대쉬를 누른 시점
    float lastAttackTime, lastSkillTime, lastDashTime;
    
    //플레이어가 공격 중인지, 아니면 대시 공격을 하고있는지 저장.
    public bool attacking = false;

    public bool dashing = false;
    
    // 터치패드에서 위아래 = v , 좌우 = h
    float h, v;

    private void Start()
    {
        //플레이어 게임 오브젝트에 붙어있는 Animator 클래스와 PlayerAttack클래스를 변수에 할당.
        avatar = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // 터치패드의 방향이 변경되면 OnStickChanged 함수가 호출.
    public void OnstickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }

    
    public void OnAttackDown()//공격중을 체크하는 함수
    {
        attacking = true;
        avatar.SetBool("Combo", true);
        StartCoroutine(StartAttack());
    }

    // 공격버튼에서 마우스나 손가락을 뗏을 때 호출되는 함수.
    public void OnattackUp()
    {
        avatar.SetBool("Combo", false);
        attacking = false;

    }

    // 일반 공격을 구현한 비동기함수, 공격 버튼을 누른지 1초마다 적들에게 데미지를 입힘.
    IEnumerator StartAttack()
    {
        if(Time.time - lastAttackTime > 1f)
        {
            lastAttackTime = Time.time;
            while (attacking)
            {
                avatar.SetTrigger("AttackStart");
                yield return new WaitForSeconds(1f);
            }
        }
    }

    // 스킬 공격 버튼을 눌렀을 때 호출되는 함수.
    public void OnSkillDown()
    {
        if (Time.time - lastDashTime > 1f)
        {
            lastDashTime = Time.time;
            avatar.SetTrigger("Dash");
        }
    }
    

    // 스킬 공격 버튼에서 마우스나 손가락을 떼었을 때 호출되는 함수.
    public void OnDashUp()
    {
        dashing = false;
    }
    // 대쉬 버튼을 눌렀을때 호출되는 함수
    public void OnDashDown()
    {
        if(Time.time - lastDashTime > 1f)
        {
            lastDashTime = Time.time;
            dashing = true;
            playerAttack.DashAttack();
            avatar.SetTrigger("Dash");
        }
    }

    void Update()
    {
        if (avatar) //animator가 있어야 실행 가능
        {
            float back = 1f;

            if (v < 0f) back = 1f;

            avatar.SetFloat("Speed", (h * h + v * v)); // 속도 값 전달

            Rigidbody rigidbody = GetComponent<Rigidbody>();

            if (rigidbody)
            {
                Vector3 speed = rigidbody.velocity;
                speed.x = 4 * h;
                speed.z = 4 * v;
                rigidbody.velocity = speed;
                if (h != 0f && v != 0f) // 캐릭터의 방향전환은 즉시 이루어짐, Animator전달 x, 코드상에서 자체적으로 해결.
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));

                }
            }
        }
    }
}

