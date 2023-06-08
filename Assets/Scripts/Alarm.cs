using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] public AudioSource _audioSource;

    private float _currentAlarmVolume;
    private float _deltaAlarmVolume;
    private float _minAlarmVolume;
    private float _maxAlarmVolume;
    private Coroutine _workingCoroutine;

    public bool IsWorkingCorutine { get; private set; }

    private void Start()
    {
        _audioSource= GetComponent<AudioSource>();
        _audioSource.volume = 0;
        _currentAlarmVolume = _audioSource.volume;
        _deltaAlarmVolume = 0.2f;
        _minAlarmVolume = 0f;
        _maxAlarmVolume= 1f;
    }

    private IEnumerator OnAlarm(float targetAlarmVolume)
    {
        IsWorkingCorutine = true;

        while (_currentAlarmVolume != targetAlarmVolume)
        {
            _currentAlarmVolume = Mathf.MoveTowards(_currentAlarmVolume, targetAlarmVolume, _deltaAlarmVolume * Time.deltaTime);
            _audioSource.volume = _currentAlarmVolume;

            if (_currentAlarmVolume == targetAlarmVolume)
            {
                IsWorkingCorutine = false;
                break;
            }

            yield return null;
        }
    }

    private void Update()
    {
        if (_workingCoroutine != null)
        {
            if (!IsWorkingCorutine)
            {
                if (_currentAlarmVolume == _maxAlarmVolume)
                {
                    StopCoroutine(_workingCoroutine);
                    _workingCoroutine = null;
                }
                else if (_currentAlarmVolume == _minAlarmVolume)
                {
                    StopCoroutine(_workingCoroutine);
                    _workingCoroutine = null;
                    _audioSource.Stop();
                }
            }
        }
    }

    public void RiseOnAlarm()
    {
        if (_workingCoroutine == null)
        {
            _audioSource.Play();
            _workingCoroutine = StartCoroutine(OnAlarm(_maxAlarmVolume));
        }
        else
        {
            StopCoroutine(_workingCoroutine);
            _workingCoroutine = StartCoroutine(OnAlarm(_maxAlarmVolume));
        }
    }

    public void DownOnAlarm()
    {
        if (_workingCoroutine == null)
        {
            _workingCoroutine = StartCoroutine(OnAlarm(_minAlarmVolume));
        }
        else
        {
            StopCoroutine(_workingCoroutine);
            _workingCoroutine = StartCoroutine(OnAlarm(_minAlarmVolume));
        }
    }
}
