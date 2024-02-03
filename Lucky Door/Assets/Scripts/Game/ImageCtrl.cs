using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class ImageCtrl : MonoBehaviour
{
    public static Action<bool> ImageShown;

    private AudioSource _audioSorce;
    private Image _image;

    private bool _stageFinish;
    private bool _isFunny;

    private float _elapsedTime;

    void Awake()
    {
        GameManager.ShowDoorImage += GameManager_ShowDoorImage;

        _image = GetComponent<Image>();
        _audioSorce = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(_stageFinish == false)
        {
            if(_image.color.a == 1)
            {
                _stageFinish = true;
                OnImageShown();
            }
            else
            {
                _elapsedTime += Time.deltaTime;
                _image.color = new Color(255, 255, 255, Mathf.InverseLerp(0, GameManager.ImageDelayCount, _elapsedTime));
            }
        }
    }

    void GameManager_ShowDoorImage(Sprite image, AudioClip clip, bool isFunny)
    {
        _isFunny = isFunny;
        _image.sprite = image;
        _audioSorce.clip = clip;
        _audioSorce.Play();
    }

    private void OnImageShown()
    {
        ImageShown?.Invoke(_isFunny);
    }

    void OnDestroy()
    {
        GameManager.ShowDoorImage -= GameManager_ShowDoorImage;
    }
}
