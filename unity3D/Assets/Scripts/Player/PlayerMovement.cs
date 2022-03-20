using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]//이 코드는 게임 오브젝트에 Animator가 없으면 실행 할 수 없다는것.
public class PlayerMovement : MonoBehaviour
{

    protected Animator avatar; // 게임 오브젝트에 붙어있는 Animator 컴포넌트를 가져온다.

    float lastAttackTime, lastSkillTime, lastDashTime;
    public bool attacking = false;
    public bool dashing = false;
    float h, v;

    private void Start()
    {
        avatar = GetComponent<Animator>();
    }


    public void OnstickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }

    private void Update()
    {
        if (avatar) //animator가 있어야 실행 가능
        {
            float back = 1f;
            if (v < 0f) back = -1f;

            avatar.SetFloat("Speed", (h * h + v * v)); // 속도 값 전달

            Rigidbody rigidbody = GetComponent<Rigidbody>();

            if (rigidbody)
            {
                Vector3 speed = rigidbody.velocity;
                speed.x = 4 * h;
                speed.z = 4 * v;
                rigidbody.velocity = speed;
                if(h!=0f && v != 0f) // 캐릭터의 방향전환은 즉시 이루어짐, Animator전달 x, 코드상에서 자체적으로 해결.
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));

                }
            }
        }   
    }
    public void OnAttackDown()//공격중을 체크하는 함수
    {
        attacking = true;
        avatar.SetBool("Combo", true);
        StartCoroutine(StartAttack());
    }

    public void OnattackUp()
    {
        avatar.SetBool("Combo", false);
        attacking = false;

    }
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

    public void OnSkillDown()
    {
        if (Time.time - lastDashTime > 1f)
        {
            lastDashTime = Time.time;
            dashing = true;
            avatar.SetTrigger("Dash");
        }
    }

    public void OnDashUp()
    {
        dashing = false;
    }
}

