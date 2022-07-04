using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//확인버튼 하나만 있는 다이얼로그 컨트롤러
public class DialogControllerAlert : DialogController
{
    //제목 설정을위한 Text오브젝트와 연결
    public Text LabelTitle;
    //내용 설정을 위한 Text오브젝트와 연결
    public Text LabelMessage;

    // 현재 클래스에 전달될 알림창의 데이터 객체를 선언
    DialogDataAlert Data
    {
        get;
        set;
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        //DialogManager에 현재 이 다이얼로그 컨트롤러 클래스가 확인창을 다룬다는 사실을 등록
        DialogManager.Instance.Regist(DialogType.Alert, this);
    }

    public override void build(DialogData data)
    {
        base.build(data);
        // 데이터가 없는데 build를 하면 로그를 남기고 예외처리
        if(!(data is DialogDataAlert))
        {
            Debug.Log("Invalid dialog data!");
            return;
        }
        //DialogDataAlert로 데이터를 받고 화면의 제목과 메시지의 내용을 입력
        Data = Data as DialogDataAlert;
        LabelTitle.text = Data.Title;
        LabelMessage.text = Data.Message;
    }

    public void OnclickOK()
    {
        // 확인버튼을 누르면 Callback함수 호출
        if (Data!=null&& Data.Callback != null)
        {
            Data.Callback();
        }
        // 모든 과정이 끝나면 현재 팝업을 DialogManager에서 제거
        DialogManager.Instance.Pop();
    }

    public static implicit operator DialogControllerAlert(DialogDataAlert v)
    {
        throw new NotImplementedException();
    }
}
