using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
                          
    GameObject prefab;
    /*   유니티 컴포넌트 - Script의 Prefab속성
                         public으로 두면 툴에 접근해서 Prefab을 가져오지만,
                         규모가 커지면 public으로 두기 어렵다.
                        - 그래서 아래처럼 Resources로 접근하여 가져옴
     */

    GameObject tank;
    void Start()
    {
        tank = Managers.Resource.Instantiate("Tank");

       // Managers.Resource.Destroy(tank);
        Destroy(tank, 3.0f); // 매개변수(삭제할 대상, 몇초 후)



      //  prefab = Resources.Load<GameObject>("Prefabs/Tank"); 
                                            // 경로명: Assets/Resources/ 밑부터 시작
      //  tank = Instantiate(prefab); // 객체 생성
    }

    
}
