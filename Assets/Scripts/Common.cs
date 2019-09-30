using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fashion
{
    // 옷                     티셔츠      셔츠    바지   치마   소매    몸판
    public enum ClothType { body, t_shirts, skirt,  sleeve };
	public enum PlayType { tutorial, test }

	public class Data                             // 클래스 자료형
    {
        public static string UserName;
		public static int Score = -1;             // 점수
		public static ClothType clothType;
		public static PlayType playType;
	}
}