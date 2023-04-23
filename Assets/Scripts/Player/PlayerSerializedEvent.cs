using UnityEngine;
using UnityEngine.Events;

public class PlayerSerializedEvent : MainBehaviour
{
   [field: SerializeField] public UnityEvent OnLevelUp;
   [field: SerializeField] public UnityEvent OnPauseMenu;
   [field: SerializeField] public UnityEvent OnDead;
}

