using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public static StageController Instance; //스테이지 컨트롤러의 인스턴스를 저장하는 static변수
    public int StagePoint = 0; // StagePoint는 현재 스테이지에서에서 쌓은 점수
    public Text PointText; // 현재 포인트를 출력하는 Text
    void Start()
    {
        Instance = this; // instance변수에 현재 클래스의 인스턴스를 저장
        DialogDataAlert alert = new DialogDataAlert("Start", "GameStart!",
            delegate () { Debug.Log("OK"); 
            });
        DialogManager.Instance.push(alert);
    }

    public void AddPoint(int Point)
    {
        StagePoint += Point;
        PointText.text = StagePoint.ToString();
    }

    public void FinishGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
