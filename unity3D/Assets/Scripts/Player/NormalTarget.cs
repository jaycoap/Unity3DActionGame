using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTarget : MonoBehaviour
{
    // 일반 공격을 할 때 공격 반경에 있는 적들의 리스트를 관리하는 클래스

    public List<Collider> targetList;// 공격 대상에 있는 적들의 리스트

    void Awake()
    {
        targetList = new List<Collider>(); // 오브젝트가 생성될 때 호출되는 Awake에서 targetList 배열을 초기화
    }

    private void OnTriggerEnter(Collider other) // 적 개체가 공격 반경 안에 들어오면, targetList에 해당 개체를 추가한다.
    {
        targetList.Add(other);
    }

    private void OnTriggerExit(Collider other) // 적 개체가 공격 반경에서 벗어나면 targetList에서 삭제한다.
    {
        targetList.Remove(other);  
    }


}
