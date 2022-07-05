using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//DialogDataConfirm은 예/아니요 팝업의 데이터를 저장하는 클래스
public class DialogDataConfirm : DialogData
{
    //제목을 저장하는 변수
    public string Title
    {
        get;
        private set;
    }
    //내용을 저장하는 변수
    public string Message
    {
        get;
        private set;
    }
    //팝업에서의 버튼 이벤트 작동시 콜백을 저장하는 변수
    public Action<bool> Callback
    {
        get;
        private set;
    }
    //DialogDataConfirm의 생성자로 제목, 내용, 콜백함수를 매개변수로 전달
    public DialogDataConfirm(string title, string message, Action<bool>callback = null) : base(DialogType.Confirm)
    {
        this.Title = title;
        this.Message = message;
        this.Callback = callback;
    }


}
