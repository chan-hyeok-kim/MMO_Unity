using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuaterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);

    [SerializeField]
    GameObject _player = null;

    void Start()
    {
        
    }

    void LateUpdate() // 그냥update()랑 달리, 플레이어가 이동한 다음에 업데이트하게 함
                      // 덜덜 떨리는 효과 사라짐
    {
        if (_mode == Define.CameraMode.QuaterView)
        {
            RaycastHit hit;
            if(Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f; 
                // 플레이어 위치-마우스 찍은 위치 =거리에서 조금 덜 이동한 거리 (어쨌든 플레이어보단 뒤에 있어야 하니깐)
                transform.position = _player.transform.position + _delta.normalized * dist;
                // 카메라의 위치를 플레이어 위치에서 normalized * dist 벡터만큼 이동
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform); // 무조건 해당 폼을 주시함
            }


           
        }
            
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
}
