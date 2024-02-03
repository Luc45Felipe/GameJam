using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class GameManager : MonoBehaviour
{
    public static Action<Sprite, AudioClip, bool> ShowDoorImage;

    [Header("Settings")]
    [SerializeField] [Range(1, 10)] private float _nextStageDelayCount;

    [SerializeField] [Range(1, 10)] private float _imageDelayCount;
    public static float ImageDelayCount { get; private set;}

    [Header("Resources")]
    [SerializeField] private Sprite _funnyImage;
    [SerializeField] private Sprite _sadImage;
    [SerializeField] private AudioClip _funnyClip, _sadClip;

    [Header("References")]
    [SerializeField] private GameObject _doorImage;
    [SerializeField] private GameObject _backGround;
    [SerializeField] private Text _cheatIdDoor;
    [SerializeField] private List<DoorCtrl> _doors;
    [SerializeField] private GameObject[] _doorTexts;
    
    private int _funnyDoorId;
    private AudioSource _audioSource;

    void Awake()
    {
        DoorCtrl.DoorOpened += DoorCtrl_DoorOpened;
        ImageCtrl.ImageShown += ImageCtrl_ImageShown;

        ImageDelayCount = _imageDelayCount;

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
        OnShowDoorImage(isFunnyImage);
    }

    private void ImageCtrl_ImageShown(bool isFunny)
    {
        StartCoroutine(NextStageDelay(isFunny));
    }

    IEnumerator NextStageDelay(bool isFunny)
    {
        if(isFunny)
        {
            yield return new WaitForSeconds(_nextStageDelayCount);

            if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
    }

    void OnShowDoorImage(bool isFunnyImage)
    {
        if(isFunnyImage)
        {
            ShowDoorImage?.Invoke(_funnyImage, _funnyClip, true);
        }
        else
        {
            ShowDoorImage?.Invoke(_sadImage, _sadClip, false);
        }
    }

    void OnDestroy()
    {
        DoorCtrl.DoorOpened -= DoorCtrl_DoorOpened;
        ImageCtrl.ImageShown -= ImageCtrl_ImageShown;
    }
}
