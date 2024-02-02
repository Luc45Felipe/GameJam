using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class GameManager : MonoBehaviour
{
    public static Action<Sprite, AudioClip> ShowFunny;
    public static Action<Sprite, AudioClip> ShowSad;

    [SerializeField] private List<DoorCtrl> _doors;

    [SerializeField] private Sprite _funnyImage, _sadImage;
    [SerializeField] private AudioClip _funnyClip, _sadClip;
    
    private AudioSource _audioSource;

    [SerializeField] private GameObject _doorImage;
    [SerializeField] private GameObject _backGround;

    [SerializeField] private int _funnyDoorId;

    [SerializeField] private GameObject[] _doorTexts;
    [SerializeField] private Text _cheatIdDoor;

    void Awake()
    {
        DoorCtrl.DoorOpened += DoorCtrl_DoorOpened;

        _audioSource = GetComponent<AudioSource>();

        SetFunnyDoor();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    void DoorCtrl_DoorOpened(DoorCtrl door)
    {
        foreach (var item in _doorTexts)
        {
            item.SetActive(false);
        }

        _audioSource.Stop();

        _backGround.SetActive(false);
        if(door == _doors[_funnyDoorId])
        {
            ShowImage(true);
        }
        else
        {
            ShowImage(false);
        }
    }

    void SetFunnyDoor()
    {
        _funnyDoorId = UnityEngine.Random.Range(0,3);
        _cheatIdDoor.text = (_funnyDoorId + 1).ToString();
    }

    void ShowImage(bool isFunnyImage)
    {
        _doorImage.SetActive(true);
        
        if(isFunnyImage)
        {
            OnShowFunny();
        }
        else
        {
            OnShowSad();
        }
    }

    void OnShowFunny()
    {
        ShowFunny?.Invoke(_funnyImage, _funnyClip);
    }

    void OnShowSad()
    {
        ShowSad?.Invoke(_sadImage, _sadClip);
    }

    void OnDestroy()
    {
        DoorCtrl.DoorOpened -= DoorCtrl_DoorOpened;
    }
}
