using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
                          
    GameObject prefab;
    /*   ����Ƽ ������Ʈ - Script�� Prefab�Ӽ�
                         public���� �θ� ���� �����ؼ� Prefab�� ����������,
                         �Ը� Ŀ���� public���� �α� ��ƴ�.
                        - �׷��� �Ʒ�ó�� Resources�� �����Ͽ� ������
     */

    GameObject tank;
    void Start()
    {
        tank = Managers.Resource.Instantiate("Tank");

       // Managers.Resource.Destroy(tank);
        Destroy(tank, 3.0f); // �Ű�����(������ ���, ���� ��)



      //  prefab = Resources.Load<GameObject>("Prefabs/Tank"); 
                                            // ��θ�: Assets/Resources/ �غ��� ����
      //  tank = Instantiate(prefab); // ��ü ����
    }

    
}
