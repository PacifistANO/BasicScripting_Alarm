using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(Alarm))]

public class CheckingEntrance : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<ThirdPersonCharacter>(out ThirdPersonCharacter player))
        {
            if (!_alarm.AudioSource.isPlaying)
            {
                _alarm.AudioSource.Play();
            }
            _alarm.ChangeVolumeAlarm(_alarm.MaxAlarmVolume);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<ThirdPersonCharacter>(out ThirdPersonCharacter player))
        {
            _alarm.ChangeVolumeAlarm(_alarm.MinAlarmVolume);
        }
    }
}
