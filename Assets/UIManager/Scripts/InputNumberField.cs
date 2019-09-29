using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace Fashion.UIManager
{
	public class InputNumberField : InputFieldVR
	{
		[SerializeField]
		private Button buttonUp;
		[SerializeField]
		private Button buttonDown;
		[SerializeField]
		private int m_defaultNumber = 0;

		protected int m_curNumber;

		public int defaultNumber { get { return m_defaultNumber; } set { m_defaultNumber = value; } }

		//
		// 요약:
		//     Accessor to the onNumberChanged.
		public UnityAction<int> onNumberChanged = delegate { };
		//
		// 요약:
		//     The Unity Event to call when editing has ended.
		public UnityAction<int> onEndEditNumber = delegate { };

		protected override void Start()
		{
			base.Start();
			m_curNumber = m_defaultNumber;
			text = m_curNumber.ToString();
			onValueChanged.AddListener((string s) =>
			{
				if (string.IsNullOrEmpty(s)) s = "0";
				onNumberChanged(Convert.ToInt32(s));
			});
			onEndEdit.AddListener((string s) =>
			{
				onEndEditNumber(Convert.ToInt32(s));
			});

		}

		public void OnNumberChanged()
		{
			if (string.IsNullOrEmpty(text)) text = "0";
			int num = Convert.ToInt32(text);
			// 0으로 시작되는 경우를 바로잡기 위해
			text = num.ToString();
			m_curNumber = num;
		}

		public void OnUpButton()
		{
			int num = Convert.ToInt32(text) + 1;
			m_curNumber = num;
			text = m_curNumber.ToString();
		}

		public void OnDownButton()
		{
			int num = Convert.ToInt32(text) - 1;
			m_curNumber = num;
			text = m_curNumber.ToString();
		}
	}
}