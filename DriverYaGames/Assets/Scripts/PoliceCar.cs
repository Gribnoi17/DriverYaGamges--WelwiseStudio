using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Tweens;
using Redcode.Moroutines;
using UnityEngine.Rendering;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

[RequireComponent(typeof(Animator))]
public class PoliceCar : MonoBehaviour
{
	[SerializeField] private GameObject _shield;
	[SerializeField] private Transform _linePoint;
	[SerializeField] private float _wheelAngelsSpeed;
	[SerializeField] private float _animationDuration;
	[SerializeField] private Speedometer _spd;
	[SerializeField] private Animator _animator;
	[SerializeField] private AudioClip _clipStart;
	[SerializeField] private AudioClip _soundEngine;
	[SerializeField] private AudioClip _shieldPickUp;
    [SerializeField] private AudioClip _collission;
    [SerializeField] private AudioClip _nitroSound;
    [SerializeField] private GameObject _nitro;
	[SerializeField] private AudioSource _canvasAS;


	private float _timeShield = 5f;
	private bool _isPlaying = true;
	private int _lineIndex = 1;
	private Playable _animation;
	private bool shieldActive;
	public bool IsShieldActive;

	[HideInInspector] public float NitroTime = 3.5f;

	private AudioSource _audioSource;
	private bool _canMove;
	public bool CanMove{ set {_canMove = value;} }


	private void Start()
	{
		StartCoroutine(StartSound());
		_isPlaying= true; 
	}

	private IEnumerator StartSound()
	{
        _audioSource = GetComponent<AudioSource>();
		_audioSource.PlayOneShot(_clipStart);
        yield return new WaitForSeconds(1f);
        _audioSource.clip = _soundEngine;
		_audioSource.Play();
	}
	 private void OnEnable()
	 {
		SwipeDetection.SwipeEvent += OnSwipe;
	 }

	public IEnumerator ActivateNitro()
    {
        if (_nitro != null)
        {
            _audioSource.PlayOneShot(_nitroSound);
            _nitro.gameObject.SetActive(true);
			yield return new WaitForSeconds(NitroTime);
			_nitro.gameObject.SetActive(false);
		}	
    }

	 private void OnSwipe(Vector2 direction)
	 {
		MoveSwipe(direction);
		_isPlaying = false;
	 }

	 private void OnDestroy()
	 {
		SwipeDetection.SwipeEvent -= OnSwipe;
	 }

	public void SetShieldActionTime(float value)
	{
		_timeShield = value;
	}

	private void Update()
	{
		if (_canMove)
        {
            MoveKeyboard(KeyCode.A, -1);
            MoveKeyboard(KeyCode.D, 1);
            MoveKeyboard(KeyCode.LeftArrow, -1);
            MoveKeyboard(KeyCode.RightArrow, 1);
        }
    }

	 private void OnCollisionEnter(Collision collision)
	 {
		if(collision.gameObject.tag == "Shield")
		{
			
		    Destroy(collision.gameObject);
		    StopCoroutine(ShieldController());
			DeactivateSchield();
		    StartCoroutine(ShieldController());
		}

		else if(collision.gameObject.tag == "SpeedBooster")
		{
		    Destroy(collision.gameObject);
            EventManager.OnPlayerTookNitro();
            StartCoroutine(ActivateNitro());
		    StartCoroutine(_spd.SpeedBoosterController());
		}
		
		else if (collision.gameObject.tag == "Car")
		{
		  if(!shieldActive)
		  {
			 _canvasAS.PlayOneShot(_collission);
			 EventManager.OnPlayerDied();
			 SwipeDetection.SwipeEvent -= OnSwipe;
			 transform.parent.gameObject.SetActive(false);
			 //gameObject.SetActive(false);
			 _isPlaying= false;
		  }else
		  {
			 Destroy(collision.gameObject);
		  }
		}
	 }

	public IEnumerator ShieldController()
    {
		_audioSource.PlayOneShot(_shieldPickUp);
        shieldActive = true;
        EventManager.OnPlayerTookShield();
        if (_shield.activeSelf == false)
            _shield.SetActive(true);
        _spd.CurrentSpeed = -10;
        //StartCoroutine(_spd.SpeedUnBusterOrShielPickUp());
        yield return new WaitForSeconds(_timeShield);
        DeactivateSchield();
        StopCoroutine(ShieldController());
    }

    private void DeactivateSchield()
    {
        _shield.SetActive(false);
        shieldActive = false;
        _spd.CurrentSpeed = +10;
    }

    private void MoveKeyboard(KeyCode key, int direction)
	 {
		if ((_animation == null || _animation.PlayedTime == _animationDuration) && Input.GetKeyDown(key))
		{
		  if (direction > 0)
		  {
			if (_linePoint.GetChild(_lineIndex).name != "Right")
				StartCoroutine(OnTurnRight());
		  }
		  else
		  {
			if (_linePoint.GetChild(_lineIndex).name != "Left")
				StartCoroutine(OnTurnLeft());
		  }
		  _lineIndex = Mathf.Clamp(_lineIndex + direction, 0, _linePoint.childCount - 1);
		  var point = _linePoint.GetChild(_lineIndex);
		  _animation = transform.DoPositionX(point.transform.position.x, _animationDuration, Ease.InOutCirc).Play();
		}
	 }

	 private void MoveSwipe(Vector2 direction)
	 {
		int _tempDirection;
		if (direction.x > 0)
		{
		  _tempDirection = 1;
			if (_linePoint.GetChild(_lineIndex).name != "Right")
				StartCoroutine(OnTurnRight());
		  
		}
		else
		{
		  _tempDirection = -1;
			if (_linePoint.GetChild(_lineIndex).name != "Left")
				StartCoroutine(OnTurnLeft());
		}

		if (_animation == null || _animation.PlayedTime == _animationDuration)
		{
		  _lineIndex = Mathf.Clamp(_lineIndex + _tempDirection, 0, _linePoint.childCount - 1);
		  var point = _linePoint.GetChild(_lineIndex);
		  _animation = transform.DoPositionX(point.transform.position.x, _animationDuration, Ease.InOutCirc).Play();
		}

	 }

	 IEnumerator OnTurnRight()
	 {
		_animator.SetBool("RightTurn", true);
		_animator.SetBool("None", false);
 
		yield return new WaitForSeconds(0.04f);
		_animator.SetBool("None", true);
		_animator.SetBool("RightTurn", false);
		StopCoroutine(OnTurnRight());
	 }

	 IEnumerator OnTurnLeft()
	 {
		_animator.SetBool("LeftTurn", true);
		_animator.SetBool("None", false);

		yield return new WaitForSeconds(0.04f) ;
		_animator.SetBool("None", true);
		_animator.SetBool("LeftTurn", false);
		StopCoroutine(OnTurnLeft());
	 }
}
