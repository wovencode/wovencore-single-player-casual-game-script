using UnityEngine;
using System.Collections;

public class UIWindowBackground : MonoBehaviour 
{
    
    public Animator animator;
    public string fadeInTriggerName = "fadeIn";
    public string fadeOutTriggerName = "fadeOut";

    public void FadeIn()
    {
        animator.SetTrigger(fadeInTriggerName);
    }

    public void FadeOut()
    {
        animator.SetTrigger(fadeOutTriggerName);
    }
}