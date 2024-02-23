using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // 1. ������ RigiBody �־�� �Ѵ� (isKinematic : off)
    // 2. ������ Collider�� �־�� �Ѵ� (isTrigger : off)
    // 3. ������� Collider�� �־�� �Ѵ� (isTrigger : off)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision @ {collision.gameObject.name} !");
    }

    // 1. �� �� Collider�� �־�� �Ѵ�
    // 2. �ּ� �� �� �ϳ��� isTrigger�� �Ѿ� ��
    // 3. �� �� �ϳ��� RigiBody �־�� �Ѵ�

    // Ư�� ���� ���� �� �޽���, ���� �̵�, ��Ʈ������ ���� ��ų
    // ���̶� ���Ͱ� �ε����� �� ������ ��� ��� ������ �Ÿ��� ���ù�����
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
