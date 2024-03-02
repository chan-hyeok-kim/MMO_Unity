using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx 
{
    public BaseScene CurrentScene
    {    // BaseScene 컴포넌트를 들고있는 애를 찾아주세요
        get {  return GameObject.FindObjectOfType<BaseScene>(); }
    }
    


    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();

        /* Define.Scene에 있는 이름과 일치하는
         * 실제 유니티 Scenes에 있는 씬 가져옴
        */
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        // 현재 사용하던 씬 날려줌
        CurrentScene.Clear();
    }
}
