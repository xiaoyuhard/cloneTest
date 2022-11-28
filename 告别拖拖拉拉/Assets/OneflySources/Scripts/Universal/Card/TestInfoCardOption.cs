using OneFlyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Card;
public class TestInfoCardOption : CardOptionControl
{

    public void CardAddEvent(SceneObj so)
    {

        string cardName = so.Name;
        if (cardName != null && cardName.Equals("TestInfo"))
        {

            UIManager.Instance.SetVisible(UIName.UIScenePurpose, true);

        }

    }

    public void CardRemoveEvent(SceneObj so)
    {

        string cardName = so.Name;
        if (cardName != null && cardName.Equals("TestInfo"))
        {

            UIManager.Instance.SetVisible(UIName.UIScenePurpose, false);

        }


    }
}
