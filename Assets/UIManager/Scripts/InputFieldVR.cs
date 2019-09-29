using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using VRKeyboard.Utils;

namespace Fashion.UIManager
{
	public class InputFieldVR : InputField
	{
		public KeyboardManager keyboardManager { get; set; }
		
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			keyboardManager.gameObject.SetActive(true);
			keyboardManager.inputField = this;
			EventSystem.current.firstSelectedGameObject = keyboardManager.gameObject;
		}

		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
		}

		public void Submit()
		{
			SendOnSubmit();
		}
	}
}