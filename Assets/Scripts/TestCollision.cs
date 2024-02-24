using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        //Local <-> World <-> ViewPort <-> Screen(ȭ��) 
        // Screen: ����Ƽ ���ʾƷ� ��, 2D�� X,Y��ǥ�� ���. �������� ��ġ�� ��Ÿ��
        // ViewPort: Screen�� ����ϳ�, ��ġ�� �����θ� ǥ������

        //���� ��ǥ(�÷��̾ �ٶ󺸴� ������ ������ǥ��)
        /* Vector3 look = transform.TransformDirection(Vector3.forward);

         Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

         RaycastHit[] hits;
         hits = Physics.RaycastAll(transform.position, look, 10);

         foreach(RaycastHit hit in hits)
         {
             Debug.Log($"RayCast! {hit.collider.gameObject.name} !");
         }*/

        //4-5. ����
        //Debug.Log(Input.mousePosition);
        //Debug.Log(Camera.main.ScreenToViewportPoint( Input.mousePosition ));


        //4-6. RayCasting2
        //(1) Ǯ� ���
        /*if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 dir = mousePos - Camera.main.transform.position;

            dir = dir.normalized; //������ �Ȱ����� ũ��� 1�� �ַ� ���߱�

            Debug.DrawRay(Camera.main.transform.position, dir * 100.0f , Color.red, 1.0f);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }
        }*/

        // (2) ray �̿�
        // ���� ���뿹��: ���� ����� �� �� ��ġ�� �÷��̾� �̵�
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            //4-7. Layer Mask
            int mask = (1 << 8) | (1 << 9); // or�����̶� ���� 768�̶� ���� ����
            //int mask = (1 << 9); 1�� �������� 8�� �δ�

            LayerMask layerMask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall"); // int�� �ڵ���ȯ��

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.tag}");

            }


           
        }


    }
}
