using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour {

    [Header("STYLE OBJECT")]
	[SerializeField]
	GameObject styleObject = null;

    [Header("STYLE PARENT")]
	[SerializeField]
	GameObject stylePanel = null;

	[Header("NEXT SCENE")]
	[SerializeField]
	string nextSceneName = null;

	// [Header("PANEL ANIMS")]
	private string panelFadeIn = "Panel Open";
    private string panelFadeOut = "Panel Close";
    private string styleExpand = "Expand";

    private Animator currentPanelAnimator;
    private Animator styleAnimator;

	float timer = 0;

    void Start ()
    {
        currentPanelAnimator = stylePanel.GetComponent<Animator>();
        currentPanelAnimator.Play(panelFadeIn);

		styleAnimator = styleObject.GetComponent<Animator>();
		styleAnimator.Play(styleExpand);
    }

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer > 4)
		{
			timer = 0;
			SceneManager.LoadScene(nextSceneName);
		}
	}
}
