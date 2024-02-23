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
                 //���� ��ǥ(�÷��̾ �ٶ󺸴� ������ ������ǥ��)
        Vector3 look = transform.TransformDirection(Vector3.forward);

        Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, look, 10);

        foreach(RaycastHit hit in hits)
        {
            Debug.Log($"RayCast! {hit.collider.gameObject.name} !");
        }
    }
}
