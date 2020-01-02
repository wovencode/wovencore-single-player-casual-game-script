using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UIPopup : MonoBehaviour
{

    public Action confirm;
    public Action cancel;
    public UIWindowBackground background;
    public Animator animator;
    public string showTriggerName = "show";
    public string closeTriggerName = "close";
    public Text tittle;
    public Text description;
    public Text confirmButtonText;
    public Text cancelButtonText;

    private void Start()
    {
        SetPopupActive(false);
    }

    public void Setup(string tittleText, string descriptionText, string confirmText, string cancelText, Action confirm, Action cancel, bool hasCancelButton)
    {
        Show();

        tittle.text = tittleText;
        description.text = descriptionText;
        this.confirm = confirm;
        this.cancel = cancel;
        this.confirmButtonText.text = confirmText;
        this.cancelButtonText.text = cancelText;

        if (!hasCancelButton)
            cancelButtonText.transform.parent.gameObject.SetActive(false);
        else
            cancelButtonText.transform.parent.gameObject.SetActive(true);
    }

    private void Show()
    {
        SetPopupActive(true);
        animator.SetTrigger(showTriggerName);
        background.FadeIn();
    }

    public void Confirm()
    {
        if(confirm != null)
            confirm();

        Close();
    }

    public void Cancel()
    {
        if(cancel != null)
            cancel();

        Close();
    }

    private void Close()
    {
        animator.SetTrigger(closeTriggerName);
        background.FadeOut();
        StartCoroutine(Tools.GetMethodName(DeactivateWindow));
    }                               

    private IEnumerator DeactivateWindow()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SetPopupActive(false);
    }

    private void SetPopupActive(bool active)
    {
        gameObject.SetActive(active);
        background.gameObject.SetActive(active);
    }
}