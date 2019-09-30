/***
 * Author: Yunhan Li
 * Any issue please contact yunhn.lee@gmail.com
 ***/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Fashion.UIManager;

namespace VRKeyboard.Utils
{
    public class KeyboardManager : MonoBehaviour
    {
        #region Public Variables
        [Header("User defined")]
        [Tooltip("If the character is uppercase at the initialization")]
		[SerializeField]
		bool isUppercase = false;
		[SerializeField]
		bool isNumeric = false;
		[SerializeField]
		int maxInputLength = 30;

        [Header("UI Elements")]
		[SerializeField]
		public InputFieldVR inputField = null;
		[SerializeField]
		Image backspaceKey = null;
		[SerializeField]
		Image capsLockKey = null;
		[SerializeField]
		Image shiftKey = null;
		[SerializeField]
		Image clearKey = null;
		[SerializeField]
		Image enterKey = null;

        [Header("Essentials")]
        public Transform keys;

		[Header("디버그용")]
		public Text debugText;
		#endregion

		#region Private Variables
		private string Input
        {
            get { return inputField.text; }
            set { inputField.text = value; }
        }
        private Key[] keyList;
        private bool capslockFlag;
		private bool shiftFlag;
		private Color backspaceColor;
		private Color capsLockColor;
		private Color shiftColor;
		private Color enterColor;
		private Color clearColor;
		private Color onColor;
		#endregion

		#region Monobehaviour Callbacks
		void Awake()
        {
            keyList = keys.GetComponentsInChildren<Key>();
			debugText.gameObject.SetActive(false);
			onColor = new Color(0.125f, 0, 1);
			backspaceColor = backspaceKey.color;
			enterColor = enterKey.color;
			if (!isNumeric)
			{
				capsLockColor = capsLockKey.color;
				shiftColor = shiftKey.color;
				clearColor = clearKey.color;
			}
		}

		void Start()
        {
            foreach (var key in keyList)
            {
                key.OnKeyClicked += GenerateInput;
            }
            capslockFlag = isUppercase;
			shiftFlag = true;
			if (!isNumeric)
				CapsLock();
        }
		#endregion

		#region Private Method
		IEnumerator SetClickColor(Image image, Color color)
		{
			image.color = onColor;
			yield return new WaitForSeconds(0.2f);
			image.color = color;
		}
		#endregion

		#region Public Methods
		public void Backspace()
        {
			StartCoroutine(SetClickColor(backspaceKey, backspaceColor));
			if (Input.Length > 0)
            {
                Input = Input.Remove(Input.Length - 1);
				if (isNumeric && Input.Length == 0)
					Input = "0";
            }
            else
            {
                return;
            }
        }

        public void Clear()
        {
			StartCoroutine(SetClickColor(clearKey, clearColor));
			Input = "";
		}

        public void CapsLock()
        {
            foreach (var key in keyList)
            {
                if (key is Alphabet)
                {
                    key.CapsLock(capslockFlag);
                }
            }
			capsLockKey.color = capslockFlag ? onColor : capsLockColor;
			capslockFlag = !capslockFlag;
        }

        public void Shift()
        {
            foreach (var key in keyList)
            {
                if (key is Shift)
                {
                    key.ShiftKey();
                }
            }
			shiftKey.color = shiftFlag ? onColor : shiftColor;
			shiftFlag = !shiftFlag;
		}

		private void OnEnable()
		{
			// 키보드가 중복 생성되지 않도록 다른 키보드 끄기
			KeyboardManager[] kms = FindObjectsOfType<KeyboardManager>();
			foreach (var km in kms)
			{
				if (km.gameObject != gameObject)
					km.gameObject.SetActive(false);
			}

		}

		public void Enter()
		{
			StartCoroutine(SetClickColor(enterKey, enterColor));
			inputField.Submit();
			gameObject.SetActive(false);
		}

		public void GenerateInput(string s)
        {
			if (Input.Length > maxInputLength) { return; }
			Input += s;
        }
        #endregion
    }
}