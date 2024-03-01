using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }

    void Start()
    {
        Init();   
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach(Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);
        

        for(int i=0; i< 8; i++) // 8���� ������+ĭ �����
        {
            GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item"); //�������� ������� ����
            item.transform.SetParent(gridPanel.transform); // �׸����г��� �θ��

            UI_Inven_Item inven_Item = Util.GetOrAddComponent<UI_Inven_Item>(item); // �����տ� ������Ʈ �ٿ��ֱ�
            inven_Item.SetInfo($"Sword NO.{i}");
        }

    }
    void Update()
    {
        
    }
}
