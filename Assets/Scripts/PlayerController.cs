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



        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;

        Tank tank1 = new Tank();
        tank1.speed = 11.0f;  
        Tank tank2 = new Tank();
        tank2.speed = 22.0f;
        Tank tank3 = new Tank();
        Tank tank4 = new Tank();
        Tank tank5 = new Tank();


    }

    /* GameObject(Player)
          Transform
          PlayerController(*) 
     */
    float _yAngle = 0.0f;
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

        


    }

    void OnKeyboard()
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
    }
}
