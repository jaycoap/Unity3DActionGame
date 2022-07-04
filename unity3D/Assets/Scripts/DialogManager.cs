using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//다이얼로그의 종류를 구분하는 enum변수
public enum DialogType
{
    Alert,
    Confirm,
    Ranking
}
public sealed class DialogManager
{
    // 플레이어에게 보여줄 팝업창들을 저장해 놓는 리스트
    List<DialogData> _dialogQueue;
    // 다이얼로그 타입에 따른 컨트롤러를 매핑한 Dictionary
    Dictionary<DialogType, DialogController> _dialogMap;
    // 현재화면에 떠있는 다이얼 로그를 지정
    DialogController _currentDialog;
    // 싱글톤 패턴으로 하나의 인스턴스를 전역적으로 공유하기 위해 instance를 생성
    private static DialogManager instance = new DialogManager();

    public static DialogManager Instance
    {
        get
        {
            return instance;
        }
    }

    // 생성자로써 클래스츼 인스턴스가 생성될 떄 인스턴스 변수들을 초기화 해줌
    private DialogManager()
    {
        _dialogQueue = new List<DialogData>();
        _dialogMap = new Dictionary<DialogType, DialogController>();

    }
    // Regist함수로 특정 dialogtype에 매칭되는 DialogController를 지정.
    public void Regist(DialogType type, DialogController controller)
    {
        _dialogMap[type] = controller;
    }

    //Push 함수로 DialogData를 추가.
    public void push(DialogData data)
    {
        // 다이얼로그 리스트를 저장하는 변수에 새로운 다이얼 로그 데이터를 추가합니다.
        _dialogQueue.Add(data);

        if (_currentDialog == null)
        {
            ShowNext();
        }
    }

    //Pop함수로 리스트에서 마지막으로 열린 다이얼로그를 닫는다.
    public void Pop()
    {
        if (_currentDialog != null)
        {
            _currentDialog.Close
                (
                    delegate
                    {
                        _currentDialog = null;

                        if (_dialogQueue.Count > 0)
                        {
                            ShowNext();
                        }
                    }
                );

        }
    }

    private void ShowNext()
    {
        // 다이얼로그 리스트에서 첫 번째 멤버를 가져옴.
        DialogData next = _dialogQueue[0];

        // 가져온 멤버의 다이얼고르 유형을 확인, 그 다이얼로그유형에 맞는 다이얼로그 컨트롤러를 조회.
        DialogController controller = _dialogMap[next.Type].GetComponent<DialogController>();

        //조회한 다이얼 로그 컨트롤러를 현재 열린 팝업의 다이얼로그 컨트롤러로 지정
        _currentDialog=controller;

        //현재열린 다이얼로그 데이터를 화면에 표시
        _currentDialog.build(next);

        //다이얼로그를 화면에 보여주는 애니메이션을 시작
        _currentDialog.Show(delegate { });

        //다이얼로그 리스트에서 꺼내온 데이터를 제거
        _dialogQueue.RemoveAt(0);

    }

    //현재 팝업 윈도우가 표시되어 있는지 확인하는 함수.
    public bool IsShowing()
    {
        return _currentDialog != null;
    }

}
