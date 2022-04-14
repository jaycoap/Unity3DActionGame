using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // 플레이어 공격력 수치
    // 레벨 / 경험치 파츠 업그레이드 챕터에서 캐릭터 성장 시스템 도입시 변경

    public int NormalDamage = 10;
    public int SkillDamage = 30;
    public int DashDamage = 30;

    // 캐릭터의 공격 반경
    // 타켓의 Trigger로 어떤 몬스터가 공격 반경안에 들어왔는지 판정.

    public NormalTarget normalTarget;
    public SkillTarget skillTarget;

    public void NormalAttack()
    {
        // normalTarget에 붙어있는 Trigger Collider에 들어있는 몬스터의 리스트를 조회
        List<Collider>TargetList = new List<Collider>(normalTarget.targetList);
        //타겟 리스트 안에 있는 몬스터들을 foreach문으로 조회
        foreach(Collider one in TargetList)
        {
            EnemyHealth enemy =one.GetComponent<EnemyHealth>(); // 타겟의 게임 오브젝트에 EnemyHealth라는 스크립트를 가져옴.
            if (enemy != null) // 만약 EnemyHealth라는 스크립트가 있다면
            {
                // 몬스터에게 데미지, 넉백을 줌.
                StartCoroutine(enemy.StartDamage(NormalDamage, transform.position, 0.5f, 0.5f));
            }
        }
    }

    public void DashAttack()
    {
        // normalTarget에 붙어있는 Trigger Collider에 들어있는 몬스터의 리스트를 조회
        List<Collider> TargetList = new List<Collider>(normalTarget.targetList);
        //타겟 리스트 안에 있는 몬스터들을 foreach문으로 조회
        foreach (Collider one in TargetList)
        {
            EnemyHealth enemy = one.GetComponent<EnemyHealth>(); // 타겟의 게임 오브젝트에 EnemyHealth라는 스크립트를 가져옴.
            if (enemy != null) // 만약 EnemyHealth라는 스크립트가 있다면
            {
                // 몬스터에게 대쉬데미지, 넉백을 줌.
                StartCoroutine(enemy.StartDamage(DashDamage, transform.position, 1f, 2f));
            }
        }
    }
    public void SkillAttack()
    {
        // normalTarget에 붙어있는 Trigger Collider에 들어있는 몬스터의 리스트를 조회
        List<Collider> TargetList = new List<Collider>(normalTarget.targetList);
        //타겟 리스트 안에 있는 몬스터들을 foreach문으로 조회
        foreach (Collider one in TargetList)
        {
            EnemyHealth enemy = one.GetComponent<EnemyHealth>(); // 타겟의 게임 오브젝트에 EnemyHealth라는 스크립트를 가져옴.
            if (enemy != null) // 만약 EnemyHealth라는 스크립트가 있다면
            {
                // 몬스터에게 스킬 데미지, 넉백을 줌.
                StartCoroutine(enemy.StartDamage(DashDamage, transform.position, 1f, 2f));
            }
        }
    }
}
