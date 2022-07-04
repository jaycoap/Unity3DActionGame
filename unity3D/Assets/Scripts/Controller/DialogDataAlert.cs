using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogDataAlert : DialogData
{
    
    public string Title
    {
        get;
        private set;
    }

    public string Message
    {
        get;
        private set;
    }

    public Action Callback
    {
        get;
        private set;
    }

    public DialogDataAlert(string title, string message, Action callback = null) : base(DialogType.Alert)
    {
        Title = title;
        Message = message;
        Callback = callback;
    }
}
