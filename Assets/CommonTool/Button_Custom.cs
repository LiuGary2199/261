using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> 自定义按钮 </summary>
public class Button_Custom : Button
{
    [Header("图片灰色材质")][SerializeField] public Material GrayMat; //灰色材质
    [Header("文字原本颜色")][SerializeField] public Color TextColor = Color.white; //文字颜色
    bool IsGray; //按钮是否禁用 是否需要变灰

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        //根据按钮是否禁用 把按钮及其子物体变灰
        IsGray = state == SelectionState.Disabled ? true : false;
        SetGray(transform);
    }

    private void SetGray(Transform Item) //图片换灰色材质 文字改灰色
    {
        Image image = Item.GetComponent<Image>();
        if (image != null)
            image.material = IsGray ? GrayMat : null;
        Text text = Item.GetComponent<Text>();
        if (text != null)
            text.color = IsGray ? Color.white : TextColor;

        // 如果还有子物体，递归调用
        if (Item.childCount > 0)
        {
            for (int i = 0; i < Item.childCount; i++)
            {
                SetGray(Item.GetChild(i));
            }
        }

    }
}

