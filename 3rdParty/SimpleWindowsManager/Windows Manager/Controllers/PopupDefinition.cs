using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class PopupDefinition
{

    public string tittleText;
    public string descriptionText;
    public string confirmText;
    public bool hasCancelButton = true;
    public string cancelText;

    public string TittleText
    {
        get { return tittleText; }
    }

    public string DescriptionText
    {
        get { return descriptionText; }
    }

    public string ConfirmText
    {
        get { return confirmText; }
    }

    public bool HasCancelButton
    {
        get { return hasCancelButton; }
    }

    public string CancelText
    {
        get { return cancelText; }
    }
}