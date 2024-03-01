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
        

        for(int i=0; i< 8; i++) // 8개의 아이템+칸 만들기
        {
            GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item"); //프리팹을 기반으로 생성
            item.transform.SetParent(gridPanel.transform); // 그리드패널을 부모로

            UI_Inven_Item inven_Item = Util.GetOrAddComponent<UI_Inven_Item>(item); // 프리팹에 컴포넌트 붙여주기
            inven_Item.SetInfo($"Sword NO.{i}");
        }

    }
    void Update()
    {
        
    }
}
