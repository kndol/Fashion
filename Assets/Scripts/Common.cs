using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fashion
{
	// 제작 상태                                  
	public enum Making_State
	{
		start, Design_Select,  //시작         디자인 선택
		original_form, dart_pos,       //원형         다트위치 
		arrangement, bundle,         //배치         마름질
		seam, cutting,        //시접선       재단
		wearing, End
	};          //착장         완성

	// 플레이 모드          시작   튜토리얼  테스트
	public enum Play_Mode { start, tutorial, test };

	public class Data
	{          //클래스 자료형
		public static int Score = 100;            //점수
		public static float Size;                 //치수
		public static float Result_Size;          //결과치수
		public static string[,] Cloth_List;       //옷 리스트
		public static bool isCheck = false;       //버튼 확인용 자료형
		public static Making_State MS = Making_State.start;
		public static Play_Mode PM = Play_Mode.start;
	}
}
