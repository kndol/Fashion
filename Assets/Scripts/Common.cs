using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fashion
{
    // 플레이 모드          시작   튜토리얼  테스트
    public enum Play_Mode { start, tutorial, test };

    // 옷                      시작    티셔츠   셔츠   바지   치마    소매   몸판
    public enum Cloth_State { start, t_shirts, shirts, pants, skirt, Sleeve, Body};

	public class Data                             //클래스 자료형
    {          
		public static int Score = 100;            //점수
		public static float Size;                 //치수
		public static float Result_Size;          //결과치수
        public static Play_Mode PM = Play_Mode.start;
        public static Cloth_State CS = Cloth_State.start;
	}
}
