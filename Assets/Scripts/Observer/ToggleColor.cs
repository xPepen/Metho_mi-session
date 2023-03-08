using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColor : MonoBehaviour
{
   public SimpleButton button;
   public TMP_Text text;
   public Image image;
   private void Awake()
   {
      button.OnSubscribe(() =>
      {
         image.color = image.color.r == 0 ? Color.white : Color.green;
      });
   }

   internal void Toggle()
   {
      text.text = text.text[0] == 'W' ? "Hello" : "World";
   }
 
}
