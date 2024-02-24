using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        //Local <-> World <-> ViewPort <-> Screen(화면) 
        // Screen: 유니티 왼쪽아래 뷰, 2D라서 X,Y좌표만 취급. 직접적인 수치로 나타남
        // ViewPort: Screen과 비슷하나, 수치를 비율로만 표시해줌

        //월드 좌표(플레이어가 바라보는 방향을 월드좌표로)
        /* Vector3 look = transform.TransformDirection(Vector3.forward);

         Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

         RaycastHit[] hits;
         hits = Physics.RaycastAll(transform.position, look, 10);

         foreach(RaycastHit hit in hits)
         {
             Debug.Log($"RayCast! {hit.collider.gameObject.name} !");
         }*/

        //4-5. 투영
        //Debug.Log(Input.mousePosition);
        //Debug.Log(Camera.main.ScreenToViewportPoint( Input.mousePosition ));


        //4-6. RayCasting2
        //(1) 풀어쓴 방법
        /*if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 dir = mousePos - Camera.main.transform.position;

            dir = dir.normalized; //방향은 똑같은데 크기는 1인 애로 맞추기

            Debug.DrawRay(Camera.main.transform.position, dir * 100.0f , Color.red, 1.0f);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }
        }*/

        // (2) ray 이용
        // 실전 응용예시: 땅을 찍었을 때 그 위치로 플레이어 이동
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            //4-7. Layer Mask
            int mask = (1 << 8) | (1 << 9); // or연산이라서 숫자 768이라 볼수 있음
            //int mask = (1 << 9); 1을 왼쪽으로 8번 민다

            LayerMask layerMask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall"); // int로 자동변환됨

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.tag}");

            }


           
        }


    }
}
