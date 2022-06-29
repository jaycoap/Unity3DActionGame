using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일반 팝업창과 확인 팝업창을 관리하는 DialogController***의 부모 클래스

public class DialogData
{
    public DialogType Type { get; set; }

    public DialogData(DialogType type)
    {
        this.Type = type;
    }
 }
public enum DialogType
{
    alert,
    confirm,
    Ranking
}
public class DialogController : MonoBehaviour
{
    public Transform window;

    public bool Visible
    {
        get
        {
            return window.gameObject.activeSelf;
        }
        private set
        {
            window.gameObject.SetActive(value);
        }
    }

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {

    }

    IEnumerator OnEnter(Action callback)
    {
        Visible = true;
        if (callback != null)
        {
            callback();
        }
        yield break;
    }

    IEnumerator OnExit(Action callback)
    {
        Visible = false;
        if (callback != null)
        {
            callback();
        }
        yield break;
    }

    public virtual void build(DialogData data)
    {

    }

    public void Show(Action callback)
    {
        StartCoroutine(OnEnter(callback));
    }

    public void Close(Action callback)
    {
        StartCoroutine(OnExit(callback));
    }
}
