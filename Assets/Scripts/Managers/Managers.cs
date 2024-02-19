using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers: MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } }

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    // Start is called before the first frame update
    void Start()
    {
       /*  여러 객체가 생성되더라도
         내부적으로는 @Mangers이름이 붙은 컴포넌트만
         생성되게 함
       */
        Init();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null) 
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go); // 함부로 삭제되지 않게 설정
            s_instance = go.GetComponent<Managers>();
        }
   
    }
}
