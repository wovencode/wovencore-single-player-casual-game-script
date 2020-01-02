using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class UIWindow : MonoBehaviour, IWindow
{
    
    public UIWindowBackground background;
    public Animator animator;
    public string showTriggerName = "show";
    public string closeTriggerName = "close";
    public Text titleText;
    public string windowTitle = "title";
    
    public Button backButton;
    public Button closeButton;
    
    public bool hasBackButton;
    public bool hasCloseButton;

    private void Start()
    {
        titleText.text = windowTitle;

        if (!WindowsController.Instance.NavigationHistory)
            backButton.gameObject.SetActive(false);
		
		backButton.gameObject.SetActive(hasBackButton);
		closeButton.gameObject.SetActive(hasCloseButton);
		
        SetWindowActive(false);
    }

    public void Unhide()
    {
        Setup();
        SetWindowActive(true);
        animator.SetTrigger(showTriggerName);
        background.FadeIn();
    }

    public void Hide()
    {
        animator.SetTrigger(closeTriggerName);
        background.FadeOut();
        StartCoroutine(Tools.GetMethodName(DeactivateWindow));
    }

    public void Show()
    {
        Unhide();
        WindowsController.Instance.RegisterWindow(this);
    }

    public void Close()
    {
        WindowsController.Instance.DeleteNavigationHistory();
        Hide();
    }

    private IEnumerator DeactivateWindow()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SetWindowActive(false);
    }

    private void SetWindowActive(bool active)
    {
        gameObject.SetActive(active);
        background.gameObject.SetActive(active);
    }

    public void Back()
    {
        WindowsController.Instance.ReturnToLastWindow();
    }

    protected abstract void Setup();
}