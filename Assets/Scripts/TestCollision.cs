using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // 1. 나한테 RigiBody 있어야 한다 (isKinematic : off)
    // 2. 나한테 Collider가 있어야 한다 (isTrigger : off)
    // 3. 상대한테 Collider가 있어야 한다 (isTrigger : off)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision @ {collision.gameObject.name} !");
    }

    // 1. 둘 다 Collider가 있어야 한다
    // 2. 최소 둘 중 하나는 isTrigger를 켜야 함
    // 3. 둘 중 하나는 RigiBody 있어야 한다

    // 특정 지역 입장 시 메시지, 지역 이동, 도트데미지 장판 스킬
    // 검이랑 몬스터가 부딪혔을 때 데미지 계산 등등 응용할 거리가 무궁무진함
    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log($"Trigger @ {other.gameObject.name} !");
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
