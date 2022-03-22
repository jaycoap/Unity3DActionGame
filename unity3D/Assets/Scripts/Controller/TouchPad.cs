using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;


public class TouchPad : MonoBehaviour
{
    private RectTransform _TouchPad;//_touchPad오브젝트와 연결

    private int _touchId = -1;//터치 입력중에 방향 컨트롤러의 영역 안에 있는 입력을 구분하기 위한 아이디

    private Vector3 _startPos = Vector3.zero;//입력이 시작되는 좌표

    public float _dragRadius = 60f;//방향 컨트롤러가 원으로 움직이는 반지름


    // 플레이어의 움직임을 관리하는 PlayerMovement 스크립트와 연결
    // 방향키가 변경되면 캐릭터에게 신호를 보내야하므로 사용
    public PlayerMovement _player;

    private bool _buttonPressed = false;//버튼이 눌렸는지 체크하는 변수

    void Start()
    {
        _TouchPad = GetComponent<RectTransform>(); // 터치패드의 RectTrasform 오브젝트를 가져옴.

        _startPos = _TouchPad.position; //터치패드의 좌표를 가져옴.
    }

    public void ButtonDown() // 버튼이 눌렸는지 확인하는 함수
    {
        _buttonPressed = true;

    }

    public void ButtonUp()//버튼을 땠는지 확인
    {
        _buttonPressed = false;

        HandleInput(_startPos);//버튼이 때어졌을 때 터치패드와 좌표를 원래 지점으로 복귀.

    }

    private void FixedUpdate()
    {
        
        HandleTouchInput();//모바일에서는 터치패드 방식으로 여러 터치 입력을 받아 처리.

#if UNITY_EDITOR||UNITY_STANDALONE_OSX||UNITY_STANDALONE_WIN||UNITY_WEBPLAYER
        HandleInput(Input.mousePosition);
#endif

    }
    void HandleTouchInput()
    {
        int countId = 0; //터치아이디(touchid)의 순서를 매기기위한 번호

        if(Input.touchCount > 0) //터치 입력은 한번에 여러 개가 들어올 수 있다. 터치가 하나 이상 입력되는 실행
        {
            foreach(Touch touch in Input.touches) // 각각의 터치 입력을 하나씩 조회
            {
                countId++; //각각의 터치 입력을 하나씩 조회합니다

                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);//현재 터치 입력의 x,y 좌표

                if (touch.phase == TouchPhase.Began) //터치 입력이 방금 시작되었다면, 혹은 TouchPhase.Began이면
                {
                    if (touch.position.x <= (_startPos.x + _dragRadius)) // 그리고 터치의 좌표가 현재 방향키 범위 내에 있다면
                    {
                        _touchId = countId; // 이 터치 아이디를 기준으로 방향 컨트롤러를 조작
                    }
                }
                if(touch.phase == TouchPhase.Moved||touch.phase == TouchPhase.Stationary) // 터치 입력이 움직였다거나, 가만히 있는 상황이라면
                {
                    if(_touchId== countId) // 터치 아이디로 지정된 경우에만
                    {
                        HandleInput(touchPos); // 좌표 입력을 받아들이고
                    }
                }
                if(touch.phase == TouchPhase.Ended)// 터치 입력이 끝났는데
                {
                    if(_touchId == countId) // 입력 받고자 했던 터치 아이디라면
                    {
                        _touchId = -1; // 터치 아이디를 해제합니다.
                    }
                }
            }
        }
    }

    void HandleInput(Vector3 input)
    {
        if (_buttonPressed)//버튼이 눌러진 상황이라면
        {
            Vector3 diffVector = (input - _startPos); // 방향 컨트롤러의 기준 좌표로부터 입력 받은 좌표가 얼마나 떨어져 있는지 구함

            if(diffVector.sqrMagnitude>_dragRadius * _dragRadius)//입력 지점과 기준 좌표의 거리를 비교, 만약 최대치 보다 크다면
            {
                diffVector.Normalize(); //방향 백터의 거리를 1로 만듦

                _TouchPad.position = _startPos + diffVector * _dragRadius; // 그리고 방향 컨트롤러는 최대치 만큼만 움직이게 함.
            }
            else // 입력지점과 기준 좌표가 최대치보다 크지 않다면
            {
                _TouchPad.position = input; // 현재 입력 좌표에 방향키를 이동시킴.
            }
        }
        else //버튼이 때어진 상황이라면
        {
            _TouchPad.position = _startPos; // 방향키를 원래 위치로 되돌려 놓음.
        }
        Vector3 diff = _TouchPad.position - _startPos; // 방향키와 기준 지점의 차이를 구함.

        Vector2 normDiff = new Vector3(diff.x/ _dragRadius, diff.y / _dragRadius); //방향키의 방향을 유지한 채로, 거리를 방향만 구함.

        if(_player != null) //플레이어가 연결되어 있다면
        {
            _player.OnstickChanged(normDiff); // 플레이어에게 변경된 좌표를 전달.
        }
    }
    
}
