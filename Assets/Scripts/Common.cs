using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fashion
{
    // 옷                     티셔츠      셔츠    바지   치마   소매    몸판
    public enum ClothType { t_shirts, skirt, sleeve, body};
	public enum PlayType { tutorial, test }

	public class Data                             // 클래스 자료형
    {
        public static string UserName;
		public static int Score = 100;            // 점수
		public static int HighScore = 100;        // 최고 점수
		public static ClothType clothType = ClothType.t_shirts;
		public static PlayType playType;
	}
}