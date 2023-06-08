using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnterCheck : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<ThirdPersonCharacter>(out ThirdPersonCharacter player))
        {
            _alarm.RiseOnAlarm();
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<ThirdPersonCharacter>(out ThirdPersonCharacter player))
        {
            _alarm.DownOnAlarm();
        }

    }
}
