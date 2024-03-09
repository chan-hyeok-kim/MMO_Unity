using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
	
	int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    PlayerStat _stat;

    bool _stopSkill = false;
	
	public override void Init()
    {
		_stat = gameObject.GetComponent<PlayerStat>();
		Managers.Input.MouseAction -= OnMouseEvent;
		Managers.Input.MouseAction += OnMouseEvent;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
	}

	protected override void UpdateMoving()
	{
		// 몬스터가 내 사정거리보다 가까우면 공격
		if (_lockTarget != null)
		{
			_destPos = _lockTarget.transform.position;
			float distance = (_destPos - transform.position).magnitude;
			if (distance <= 1)
			{
				State = Define.State.Skill;
				return;
			}
		}

		// 이동
		Vector3 dir = _destPos - transform.position;
		if (dir.magnitude < 0.1f)
		{
			State = Define.State.Idle;
		}
		else
		{
			Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
			if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
			{
				if (Input.GetMouseButton(0) == false) //마우스를 누르고 있지 않다면
					State = Define.State.Idle;
				return;
			}

			float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            float smoothTime = 0.1f; 
            Vector3 currentVelocity = Vector3.zero;
			transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.LookRotation(Vector3.SmoothDamp(transform.forward, dir, ref currentVelocity, smoothTime));

           // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
		}
	}

    protected override void UpdateSkill()
	{
		if (_lockTarget != null)
		{
			Vector3 dir = _lockTarget.transform.position - transform.position;
			Quaternion quat = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
		}
	}

	void OnHitEvent()
	{
		Debug.Log("OnHitEvent");
        //Animator anim = GetComponent<Animator>();
        //anim.SetBool("attack", false);

		if(_lockTarget != null)
		{
			Stat targetStat = _lockTarget.GetComponent<Stat>();
			Stat myStat = gameObject.GetComponent<PlayerStat>();
			int damage = Mathf.Max(0, myStat.Attack - targetStat.Defense); //데미지가 음수면 0으로
			Debug.Log(damage);
			targetStat.Hp -= damage;
		}

        // TODO
        if (_stopSkill)
		{
			State = Define.State.Idle;
		}
		else
		{
			State = Define.State.Skill;
		}
	}
	
	void OnMouseEvent(Define.MouseEvent evt)
	{
		switch (State)
		{
			case Define.State.Idle:
				OnMouseEvent_IdleRun(evt);
				break;
			case Define.State.Moving:
				OnMouseEvent_IdleRun(evt);
				break;
			case Define.State.Skill:
				{
					if (evt == Define.MouseEvent.PointerUp)
						_stopSkill = true;
				}
				break;
		}
	}

	void OnMouseEvent_IdleRun(Define.MouseEvent evt)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
		//Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

		switch (evt)
		{
			case Define.MouseEvent.PointerDown:
				{
					if (raycastHit)
					{
						_destPos = hit.point;
						State = Define.State.Moving;
						_stopSkill = false;

						if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
							_lockTarget = hit.collider.gameObject;
						else
							_lockTarget = null;
					}
				}
				break;
			case Define.MouseEvent.Press:
				{
					if (_lockTarget == null && raycastHit)
						_destPos = hit.point;
				}
				break;
			case Define.MouseEvent.PointerUp:
				_stopSkill = true;
				break;
		}
	}
}

/*  virtual은 protected override로
 *  abstract는 public override로 
 *  둘의 기능적인 차이:
 *  abstract는 강제 구현
 *  virtual은 옵션
 */