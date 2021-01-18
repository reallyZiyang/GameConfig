using GameBase;
using System.Collections.Generic;
using UnityEngine;

public class Win_GameMain : WindowBase
{
    Transform scoreLayout;
    List<Widget_Number> UI_numbers = new List<Widget_Number>();

    public override void init()
    {
        base.init();
        UI_numbers.Add(newNumberUI(0));
        UI_numbers.Add(newNumberUI(1));
        UI_numbers.Add(newNumberUI(2));
        updateUI();
    }

    Widget_Number newNumberUI(int number)
    {
        Widget_Number UI_number = UIManager.Instance.newWidget(GameConfiger.Game_base, "Widget_Number", number) as Widget_Number;
        scoreLayout = child("ScoreLayout");
        UI_number.root().SetParent(scoreLayout, false);
        return UI_number;
    }

    void updateUI()
    {
        for (int index = 0; index < UI_numbers.Count; index++)
        {
            UI_numbers[index].root().localPosition = new Vector3(40 + index * 80, 0, 0);
        }
    }

}
