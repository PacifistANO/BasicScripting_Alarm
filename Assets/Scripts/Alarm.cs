using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] public AudioSource AudioSource;

    private float _currentAlarmVolume;
    private float _deltaAlarmVolume;
    private Coroutine _workingCoroutine;

    public float MinAlarmVolume { get; private set; }
    public float MaxAlarmVolume { get; private set; }

    public bool IsWorkingCorutine { get; private set; }

    private void Start()
    {
        AudioSource= GetComponent<AudioSource>();
        AudioSource.Play();
        AudioSource.volume = 0;
        _currentAlarmVolume = AudioSource.volume;
        _deltaAlarmVolume = 0.2f;
        MinAlarmVolume = 0f;
        MaxAlarmVolume= 1f;
    }

    private IEnumerator OnAlarm(float targetAlarmVolume)
    {
        while (_currentAlarmVolume != targetAlarmVolume)
        {
            _currentAlarmVolume = Mathf.MoveTowards(_currentAlarmVolume, targetAlarmVolume, _deltaAlarmVolume * Time.deltaTime);
            AudioSource.volume = _currentAlarmVolume;

            if (_currentAlarmVolume == MinAlarmVolume)
            {
                AudioSource.Stop();
            }

            if (_currentAlarmVolume == targetAlarmVolume)
            {
                StopCoroutine(_workingCoroutine);
                _workingCoroutine = null;
            }

            yield return null;
        }
    }

    public void ChangeVolumeAlarm(float targetAlarmVolume)
    {
        if (_workingCoroutine == null)
        {
            _workingCoroutine = StartCoroutine(OnAlarm(targetAlarmVolume));
        }
        else
        {
            StopCoroutine(_workingCoroutine);
            _workingCoroutine = StartCoroutine(OnAlarm(targetAlarmVolume));
        }
    }
}
