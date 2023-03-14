using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUpEvent : MainBehaviour
{
   [field: SerializeField] public UnityEvent OnLevelUp;
}
