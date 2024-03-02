using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    void Start()
    {
        Init();
    }


    protected override void Init()
    {
        base.Init();
        //TEMP

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        for(int i = 0; i < 5; i++)
            Managers.Resource.Instantiate("UnityChan");
    }

    public override void Clear()
    {
       
    }

   

}
