using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i< 5; i++) 
            list.Add(Managers.Resource.Instantiate("UnityChan"));

       foreach (GameObject obj in list) //생성한만큼 제거하기
        {
            Managers.Resource.Destroy(obj);
        }
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            /* SceneManager는 기본적으로 지원됨
             * 넘어갈 씬의 이름 적어주기
            */
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}
