using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTarget : MonoBehaviour
{
    public List<Collider> targetList; // 스킬 공격 대상에 있는 적들의 리스트

    void Awake()
    {
        targetList = new List<Collider>();// 오브젝트가 생성될 대 호출되는 targetList 배열을 초기화
    }

    void OnTriggerEnter(Collider other)
    {
        targetList.Add(other);// 적 개체가 스킬 공격 반경 안에 들어오면, targetList에 해당 개체를 추가
    }

    private void OntriigerExit(Collider other)
    {
        targetList.Remove(other); // 적 개체가 스킬 공격 반경을 벗어나면 targetList에서 개체를 제거
    }
}
