using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace Fashion.UIManager
{
	public struct InputNumberFieldParams
	{
		public InputNumberFieldParams(int _defaultNumber, int _interval, int _minNumber, int _maxNumber)
		{
			defaultNumber = _defaultNumber;
			interval = _interval;
			minNumber = _minNumber;
			maxNumber = _maxNumber;
		}
		public int defaultNumber;
		public int interval;
		public int minNumber;
		public int maxNumber;
	}

	public class InputNumberField : InputFieldVR
	{
		[SerializeField]
		private Button buttonUp;
		[SerializeField]
		private Button buttonDown;
		[SerializeField]
		private int m_defaultNumber = 0;
		[SerializeField]
		private int m_interval = 1;
		[SerializeField]
		private int m_minNumber = 0;
		[SerializeField]
		private int m_maxNumber = 100;

		protected int m_curNumber;

		public InputNumberFieldParams DefaultValues
		{
			get
			{
				return new InputNumberFieldParams(defaultNumber, interval, minNumber, maxNumber);
			}

			set
			{
				defaultNumber = value.defaultNumber;
				if (value.interval > 0)
					interval = value.interval;
				minNumber = value.minNumber;
				maxNumber = value.maxNumber;
				text = defaultNumber.ToString();
			}
		}

		public int defaultNumber { get { return m_defaultNumber; } set { m_defaultNumber = value; } }
		public int interval
		{
			get
			{
				return m_interval;
			}
			set
			{
				Assert.IsTrue(value > 0, "증감 간격은 0보다 큰 값을 가져야 합니다.");
				m_interval = value;
			}
		}
		public int minNumber { get { return m_minNumber; } set { m_minNumber = value; } }
		public int maxNumber { get { return m_maxNumber; } set { m_maxNumber = value; } }

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
				onNumberChanged(Convert.ToInt32(s));
			});
			onEndEdit.AddListener((string s) =>
			{
				onEndEditNumber(Convert.ToInt32(s));
			});

		}

		public void OnNumberChanged()
		{
			int num = Convert.ToInt32(text);
			if (num >= m_minNumber && num <= m_maxNumber)
				m_curNumber = num;
		}

		public void OnUpButton()
		{
			int num = Convert.ToInt32(text) + m_interval;
			if (num < m_maxNumber)
			{
				m_curNumber = num;
				text = m_curNumber.ToString();
			}
		}

		public void OnDownButton()
		{
			int num = Convert.ToInt32(text) - m_interval;
			if (num > m_minNumber)
			{
				m_curNumber = num;
				text = m_curNumber.ToString();
			}
		}
	}
}