using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Tank
{
    public float speed = 15.0f;
    Player player; // ���� ���� Nested Prefab(Pre_Fabrication)
}
// ����
// 1. ��ġ ����
// 2. ���� ����

class FastTank : Tank
{

}

class Player
{
    
}

struct MyVector
{
    public float x;
    public float y;
    public float z;

    public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } }
    public MyVector normalized { get { return new MyVector(x / magnitude, y / magnitude, z / magnitude); } }

    public MyVector(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }

    public static MyVector operator +(MyVector a, MyVector b)
    {
        return new MyVector( a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static MyVector operator -(MyVector a, MyVector b)
    {
        return new MyVector(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static MyVector operator *(MyVector a, float d)
    {
        return new MyVector(a.x * d, a.y * d, a.z * d);
    }
}
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f; // public���� �θ� ����Ƽ���� �ӵ� ���� ����

   
    Vector3 _destPos;
    
    void Start()
    {
        MyVector to = new MyVector(10.0f, 0.0f, 0.0f);
        MyVector from = new MyVector(5.0f, 0.0f, 0.0f);
        MyVector dir = to - from; // (5.0f, 0.0f, 0.0f);  
                                  // �̷��� �ؼ� ���� ���� ������,
                                  // ���� magnitude�� nomalized�� ���� ���� ������ ��´�
        dir = dir.normalized; // (1.0f, 0.0f, 0.0f);  ���� ���� ����, ������ ����
        MyVector newPos = from + dir * _speed;
        // ���� ���� ���� 2���� 
        // 1. �Ÿ�(ũ��) : magnitude �̿�
        // 2. ���� ���� : normalized �̿�



        // Managers.Input.KeyAction -= OnKeyboard;
        // Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;  //��������Ʈ
        Managers.Input.MouseAction += OnMouseClicked;

        //Managers.Resource.Instantiate("UI/UI_Button");

        //TEMP
        UI_Button ui = Managers.UI.ShowPopupUI<UI_Button>();

        Managers.UI.ClosePopupUI(ui);

    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;
       // if (evt != Define.MouseEvent.Click)
       //     return;

        // (2) ray �̿�
        // ���� ���뿹��: ���� ����� �� �� ��ġ�� �÷��̾� �̵�
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        //4-7. Layer Mask
        int mask = (1 << 8) | (1 << 9); // or�����̶� ���� 768�̶� ���� ����
        //int mask = (1 << 9); 1�� �������� 8�� �δ�


        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;

            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.tag}");

        }



        

    }

    /* GameObject(Player)
          Transform
          PlayerController(*) 
     */
    float _yAngle = 0.0f;
    //float wait_run_ratio = 0;
   

    public enum PlayerState
    {
        Die,
        Moving,
        Idle, 
     
    }

    PlayerState _state = PlayerState.Idle;


    /*void OnRunEvent(String a)
    {
        Debug.Log($"�ѹ� �ѹ�~~! {a}");
    }*/

    void UpdateDie()
    {
        // �ƹ��͵� ����
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Math.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);

            /*  Math.Clamp(a,b,c) �޼���
                *  : a�� ���� ������ �� b���� ������ b,
                *    c���� ũ�� c�� ����
                */
            transform.position += dir.normalized * moveDist;
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            float smoothTime = 0.1f; // ������ �ɸ��� �ð�
            Vector3 currentVelocity = Vector3.zero;
            transform.rotation = Quaternion.LookRotation(Vector3.SmoothDamp(transform.forward, dir, ref currentVelocity, smoothTime));

            //transform.LookAt(_destPos); // destPos�� �ٶ󺸱�
        }

        //�ִϸ��̼�
        //wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _speed);

        //anim.SetFloat("wait_run_ratio", wait_run_ratio); // 1
        //anim.Play("WAIT_RUN");
    }

    void UpdateIdle()
    {
        //�ִϸ��̼�
        //wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
        Animator anim = GetComponent<Animator>();
        //���� ���� ���¿� ���� ���� �Ѱ��ش�
        anim.SetFloat("speed", 0);

        //anim.SetFloat("wait_run_ratio", wait_run_ratio); // 0
        //anim.Play("WAIT_RUN");
    }



    void Update()
    {
        /*  Local ��ǥ -> World ��ǥ 
            transform.TransformDirection
            :�ڱⰡ �ٶ󺸴� ������ ������ �������� �������
            transform.TransformDirection(Vector3.forward * Time.deltaTime * speed)
            : ������ �������� ����ؼ� ���忡 ������ �� �ְ� �Ѵ�
        */

        /*  World ��ǥ -> Local ��ǥ
            InverseTransformDirection
         */
        _yAngle += Time.deltaTime * _speed;
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);
        //  ���� 360�� �Ѿ�� ���� ����� �Ʒ�ó�� Rotate�̿����ִ� ���� ����
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        // x,y,z�ุ ���� gimbal lock �̽��� ����
        // �׷��� ����, 4��° ���� �ʿ���
        //transform.rotation = Quaternion.Euler(0.0f, _yAngle, 0.0f);



       
        /* state ���� - �Ը� ���� �� ����
         *  (1) 1���� 1���� ���¸� ����
         *  (2) �����̸鼭 ��ų������ state������ ����
         *  
         */
        switch(_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
       
       



    }

   /* void OnKeyboard()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
            // ���÷� �ϸ� �÷��̾ �ٶ󺸴� �������� ���� ���󰡱� ������
            // Slerp�� �����Ѵ�� Ŀ�긦 �׸��� �ȴ�
            // �׷��� ����������� �ٲ���� �ξ� �ڿ�������
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }

        //new Vector3(1.0f, 0.0f, 0.0f) �� �� ���� �ִµ�,
        //�̰� �� �������� ���� ����
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        _moveToDest = false;
    }*/



}
