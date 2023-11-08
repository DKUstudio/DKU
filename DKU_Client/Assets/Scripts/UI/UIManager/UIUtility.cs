using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIUtilities
{
    public static void UIActive(GameObject ui, bool isActive)
    {
        if (ui != null)
        {
            ui.SetActive(isActive);
        }
    }
    
}
