using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace note
{
    internal class Lecture11
    {


    }

    /*  
/*  코루틴이란? 일종의 가벼운 스레드
    *  함수의 상태를 저장/ 복원 가능
    *  1. 엄청 오래 걸리는 작업을 잠시 끊거나
    *   원하는 타이밍에 함수를 잠시 Stop/복원하는 경우
    *  2. return -> Object라서 우리가 원하는 타입 가능
    */
    /*  class CoroutineTest : IEnumerable 
      {
          public IEnumerator GetEnumerator() 
          {
              yield return new Test() { id = 1 };
              yield return null;  // yield break하면 정지
              yield return new Test() { id = 2 };
              yield return new Test() { id = 3 };
              yield return new Test() { id = 4 };
          }

          void GenerateItem()
          {
              *//*아이템 만든다
                 1. DB저장
                 2. DB에 저장이 실패한 경우에도 로직이 실행되어버리면 
                 3. 실제 게임과 DB내용에 괴리감, 문제가 생김
                 -> 이런 경우 잠시 멈춘 후에 로직이 실행되도록 해야 한다

                 일정 시간초 기다렸다가 발동되는 스킬(ex 폭발)
              *//*
              float deltaTime = 0;
              void ExplodeAfter4Seconds()
              {
                  deltaTime += Time.deltaTime;
                  if(deltaTime > 4)
                  {
                      //로직 
                  }
              }


          }
      }
    */
    Coroutine co;

    IEnumerator ExplodeAfterSeconds(float seconds)
    {
        Debug.Log("Explode Enter");
        StartCoroutine("CoStopExplode", 2.0f);

        yield return new WaitForSeconds(seconds);

        Debug.Log("Explode Execute");


    }
    //코루틴은 앞의 'Co'처럼 구별 가능한 단어를 붙여주는 게 좋다
    IEnumerator CoStopExplode(float seconds)
    {
        Debug.Log("Stop Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Stop Execute!!!");

        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }
    }
}