using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Alarm : MonoBehaviour
{
    [SerializeField] private Light _alarmLight;
    [SerializeField] private AudioSource _audioSource;

    private float _currentAlarmVolume;
    private float _targetAlarmVolume;
    private float _deltaAlarmVolume;
    private bool _isEntered;

    private void Start()
    {
        _currentAlarmVolume = 0;
        _targetAlarmVolume = 1;
        _deltaAlarmVolume = 0.2f;
        _audioSource.Play();
        _audioSource.volume= _currentAlarmVolume;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<ThirdPersonCharacter>(out ThirdPersonCharacter player))
        {
            _isEntered = true;
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<ThirdPersonCharacter>(out ThirdPersonCharacter player))
        {
            _isEntered = false;
        }
    }

    private void Update()
    {
        if (_isEntered)
        {
            _targetAlarmVolume = 1;
            _currentAlarmVolume = Mathf.MoveTowards(_currentAlarmVolume, _targetAlarmVolume, _deltaAlarmVolume * Time.deltaTime);
            _audioSource.volume = _currentAlarmVolume;
        }
        if (!_isEntered) 
        {
            _targetAlarmVolume = 0;
            _currentAlarmVolume = Mathf.MoveTowards(_currentAlarmVolume, _targetAlarmVolume, _deltaAlarmVolume * Time.deltaTime);
            _audioSource.volume = _currentAlarmVolume;
        }
    }
}
