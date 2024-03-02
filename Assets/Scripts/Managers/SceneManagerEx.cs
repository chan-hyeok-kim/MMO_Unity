using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx 
{
    public BaseScene CurrentScene
    {    // BaseScene ������Ʈ�� ����ִ� �ָ� ã���ּ���
        get {  return GameObject.FindObjectOfType<BaseScene>(); }
    }
    


    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();

        /* Define.Scene�� �ִ� �̸��� ��ġ�ϴ�
         * ���� ����Ƽ Scenes�� �ִ� �� ������
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
        // ���� ����ϴ� �� ������
        CurrentScene.Clear();
    }
}
