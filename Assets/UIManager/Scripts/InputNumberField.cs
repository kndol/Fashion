using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Assertions;

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

public class InputNumberField : MonoBehaviour
{
	[SerializeField]
	Button buttonUp;
	[SerializeField]
	Button buttonDown;
	[SerializeField]
	int m_defaultNumber = 0;
	[SerializeField]
	int m_interval = 1;
	[SerializeField]
	int m_minNumber = 0;
	[SerializeField]
	int m_maxNumber = 100;

	InputField m_inputField;
	int m_curNumber;

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
			if (m_inputField != null)
				m_inputField.text = defaultNumber.ToString();
		}
	}

	public UnityAction<int> onEndEdit = delegate { };
	public UnityAction<int> onValueChanged = delegate { };

	public int defaultNumber { get { return m_defaultNumber; } set { m_defaultNumber = value; } }
	public int interval {
		get {
			return m_interval;
		}
		set {
			Assert.IsTrue(value > 0, "증감 간격은 0보다 큰 값을 가져야 합니다.");
			m_interval = value;
		}
	}
	public int minNumber { get { return m_minNumber; } set { m_minNumber = value; } }
	public int maxNumber { get { return m_maxNumber; } set { m_maxNumber = value; } }
	public string placeHolderText
	{
		set
		{
			if (m_inputField != null && !string.IsNullOrEmpty(value))
			{
				m_inputField.placeholder.gameObject.GetComponent<Text>().text = value;
			}
		}
	}

	private void Start()
	{
		m_inputField = GetComponent<InputField>();
		m_curNumber = m_defaultNumber;
		m_inputField.text = m_curNumber.ToString();
		m_inputField.onValueChanged.AddListener((string s) =>
		{
			onValueChanged(Convert.ToInt32(s));
		});
		m_inputField.onEndEdit.AddListener((string s) =>
		{
			onEndEdit(Convert.ToInt32(s));
		});

	}

	public void OnNumberChanged()
	{
		int num = Convert.ToInt32(m_inputField.text);
		if (num >= m_minNumber && num <= m_maxNumber)
			m_curNumber = num;
	}

	public void OnUpButton()
	{
		int num = Convert.ToInt32(m_inputField.text) + m_interval;
		if (num < m_maxNumber)
		{
			m_curNumber = num;
			m_inputField.text = m_curNumber.ToString();
		}
	}

	public void OnDownButton()
	{
		int num = Convert.ToInt32(m_inputField.text) - m_interval;
		if (num > m_minNumber)
		{
			m_curNumber = num;
			m_inputField.text = m_curNumber.ToString();
		}
	}
}
