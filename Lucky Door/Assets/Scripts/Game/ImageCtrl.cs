using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class ImageCtrl : MonoBehaviour
{
    private Image _image;

    private float _elapsedTime;

    [SerializeField] private float _nextStageDelay;
    [SerializeField] private float _imageDelay;

    private AudioSource _audioSorce;

    private bool _stageFinish;
    private bool _isFunny;

    void Awake()
    {
        GameManager.ShowFunny += GameManager_ShowFunnyImage;
        GameManager.ShowSad += GameManager_ShowSadImage;

        _image = GetComponent<Image>();
        _audioSorce = GetComponent<AudioSource>();
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        _image.color = new Color(255, 255, 255, Mathf.InverseLerp(0, _imageDelay, _elapsedTime));

        if(_image.color.a == 1 && _stageFinish == false)
        {
            _stageFinish = true;
            StartCoroutine(NextStageDelay());
        }
    }

    IEnumerator NextStageDelay()
    {
        if(_isFunny)
        {
            yield return new WaitForSeconds(_nextStageDelay);

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

    void GameManager_ShowFunnyImage(Sprite image, AudioClip clip)
    {
        _isFunny = true;
        _image.sprite = image;

        _audioSorce.clip = clip;
        _audioSorce.Play();
    }

    void GameManager_ShowSadImage(Sprite image, AudioClip clip)
    {
        _isFunny = false;
        _image.sprite = image;

        _audioSorce.clip = clip;
        _audioSorce.Play();
    }

    void OnDestroy()
    {
        GameManager.ShowFunny -= GameManager_ShowFunnyImage;
        GameManager.ShowSad -= GameManager_ShowSadImage;
    }
}
