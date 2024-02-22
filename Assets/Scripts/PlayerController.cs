using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Tank
{
    public float speed = 15.0f;
    Player player; // 포함 관계 Nested Prefab(Pre_Fabrication)
}
// 벡터
// 1. 위치 벡터
// 2. 방향 벡터

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
    float _speed = 10.0f; // public으로 두면 유니티에서 속도 변경 가능
    
    
    void Start()
    {
        MyVector to = new MyVector(10.0f, 0.0f, 0.0f);
        MyVector from = new MyVector(5.0f, 0.0f, 0.0f);
       
        MyVector dir = to - from; // (5.0f, 0.0f, 0.0f);  
                                  // 이렇게 해서 얻을 수도 있지만,
                                  // 보통 magnitude랑 nomalized로 방향 벡터 정보를 얻는다
                                  
        dir = dir.normalized; // (1.0f, 0.0f, 0.0f);  실제 방향 정보, 방향의 단위

        MyVector newPos = from + dir * _speed;

        // 방향 벡터 정보 2가지 
        // 1. 거리(크기) : magnitude 이용
        // 2. 실제 방향 : normalized 이용



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
        /*  Local 좌표 -> World 좌표 
            transform.TransformDirection
            :자기가 바라보는 방향을 로컬을 기준으로 계산해줌
            transform.TransformDirection(Vector3.forward * Time.deltaTime * speed)
            : 로컬을 기준으로 계산해서 월드에 대입할 수 있게 한다
        */

        /*  World 좌표 -> Local 좌표
            InverseTransformDirection
         */
        _yAngle += Time.deltaTime * _speed;
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);
        //  위는 360도 넘어가면 오류 생기니 아래처럼 Rotate이용해주는 것이 좋다
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        // x,y,z축만 쓰면 gimbal lock 이슈가 생김
        // 그래서 쿼터, 4번째 축이 필요함
        //transform.rotation = Quaternion.Euler(0.0f, _yAngle, 0.0f);

        


    }

    void OnKeyboard()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
            // 로컬로 하면 플레이어가 바라보는 방향으로 먼저 따라가기 때문에
            // Slerp에 설정한대로 커브를 그리게 된다
            // 그래서 월드방향으로 바꿔줘야 훨씬 자연스럽다
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

        //new Vector3(1.0f, 0.0f, 0.0f) 로 쓸 수도 있는데,
        //이게 더 가독성도 높고 편함
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }
}
