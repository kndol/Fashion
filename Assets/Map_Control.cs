using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
          // 제작 상태     시작    원형          다트위치   마름질   시접선그리기      재단      착장     완성
public enum Making_State { start, original_form, dart_pos,  bundle,     seam,        cutting,   wearing,   End};
          // 플레이 모드   시작    튜토리얼   테스트
public enum Play_Mode {    start,  tutorial,  test };

public class Data {          //클래스 자료형
    public static int        Score = 100;          //점수
    public static float      Size;                 //치수
    public static float      Result_Size;          //결과치수
    public static string     [,]Cloth_List;        //옷 리스트
}

public class Map_Control : MonoBehaviour {
    Making_State MS = Making_State.start;
    Play_Mode PM = Play_Mode.start;
    public Canvas Start_UI;
    public Canvas Mode_UI;

    private void Awake() {
       //Init_Data();                //데이터 초기화 함수
    }

    void Start() {
    }

    void Update() {
        
    }

    /*public void Start_Btn() {        //시작 버튼
        Start_UI.enabled = false;
        Mode_UI.enabled = true;
    }

    public void Exit_Btn() {         //끝내기 버튼
        Application.Quit();
    }

    public void Tutorial_Btn() {     //튜토리얼 버튼
        Mode_UI.enabled = false;
    }

    public void Test_Btn() {         //테스트 버튼
        Mode_UI.enabled = false;
    }

    public void Rank_Btn() {         //랭킹 버튼

    }

    public void Explan_Btn() {       //설명 버튼 

    }

    void Init_Data() {               //데이터 초기화 함수
        Data.Score = 100;
        Start_UI.enabled = true;
        Mode_UI.enabled = false;
    }*/
}
