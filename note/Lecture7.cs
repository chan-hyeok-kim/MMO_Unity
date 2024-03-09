using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace note
{
    internal class Lecture7
    {


    }

    /*   7강
     *   UI Manager #2
     *   1. prefab에 있는 버튼에서 블록커 설정
     *   버튼 속성들 아래에서부터 훑기 때문에 블록커가 맨 위에 있어야 한다
         배치를 잘못하면 블록커가 전부 막아버려서 팝업 이동이 아예 안됨 

         UI
         - 패널로 만든 건 이미지로도 만들 수 있음
         - 그외에도 UI는 직접 사용해보면 대부분 알 수 있는 것들
     */

    /*  인벤토리
     *  패널패널-이미지 넣어서 칸에 들어가는 아이템 이미지 넣을 수 있다
        - Alt + Shift 활용하면 칸 사이즈(부모)에 맞춰서 넣기 가능
        
        *** 여기서 중요한 유니티 기능 ***
        인벤 - 패널 - 칸(패널) 이미지 
       -  이렇게 구성되어있을 때 전체 패널 안에 칸들을 일정 간격으로 배치하고 싶으면 
        component를 추가해서 layout을 주면 된다
       - 강의에서는 grid layout사용했음 
    */

}
