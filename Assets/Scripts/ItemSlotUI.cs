using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI descTxt;

    public void Set(ItemData item)
    {
        itemIcon.sprite = item.icon;
        descTxt.text = item.desc;
    }
}