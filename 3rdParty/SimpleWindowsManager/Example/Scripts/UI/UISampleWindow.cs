using UnityEngine;
using System.Collections;

public class UISampleWindow : UIWindow
{
    protected override void Setup()
    {
    }

    public void ShowConfirmPopupWithoutCloseWindows()
    {
        WindowsController.Instance.PopupController.Show(PopupDefinitions.ConfirmExample, PrintConfirmationMessage, null);
    }

    public void ShowConfirmPopupCloseWindows()
    {
        WindowsController.Instance.PopupController.Show(PopupDefinitions.ConfirmExample, PrintConfirmationMessage, null, true);
    }

    public void ShowConfirmCancelPopupWithoutCloseWindows()
    {
        WindowsController.Instance.PopupController.Show(PopupDefinitions.ConfirmCancelExample, PrintBuyMessage, PrintCancelMessage);
    }

    public void ShowConfirmCancelPopupCloseWindows()
    {
        WindowsController.Instance.PopupController.Show(PopupDefinitions.ConfirmCancelExample, PrintBuyMessage, PrintCancelMessage, true);
    }

    private void PrintConfirmationMessage()
    {
    }

    private void PrintBuyMessage()
    {
    }

    private void PrintCancelMessage()
    {
    }
    
}