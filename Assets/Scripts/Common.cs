using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fashion
{
    // 플레이 모드          시작   튜토리얼  테스트
    public enum Play_Mode { start, tutorial, test };
    //옷                      시작    티셔츠    셔츠    바지   치마
    public enum Cloth_State { start, t_shirts, shirts, pants, skirt};
    // 제작 상태                                  
    public enum Making_State
	{
		start,              Design_Select, // 시작         디자인 선택
		original_form,      dart_pos,      // 원형         다트위치 
		arrangement,        bundle,        // 배치         마름질
		cutting,            sweing_sheet,  // 재단         낱장재봉
        sweing_fabrication, wearing,       // 합봉         착장
        End                                // 완성
    };

	public class Data                             //클래스 자료형
    {          
		public static int Score = 100;            //점수
		public static float Size;                 //치수
		public static float Result_Size;          //결과치수
		public static bool isCheck = false;       //버튼 확인용 자료형
        public static Play_Mode PM = Play_Mode.start;
        public static Cloth_State CS = Cloth_State.start;
        public static Making_State MS = Making_State.start;     
	}
}
