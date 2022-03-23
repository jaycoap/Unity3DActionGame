using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 주인공 캐릭터를 카메라가 일정한 거리를 유지한채 따라다닌다

public class FollowingCamera : MonoBehaviour
{

    public float distanceAway = 7f;
    public float distanceUp = 4f;

    public Transform follow; // 따라다닐 객체를 지정


    

    
    void LateUpdate() 
    {
        transform.position = follow.position + Vector3.up * distanceUp - Vector3.forward * distanceAway;
    }
}
