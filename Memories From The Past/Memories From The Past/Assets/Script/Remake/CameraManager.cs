using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;

namespace Remake
{
    public sealed class CameraManager : MonoBehaviour
    {
        [SerializeField] private GameObject _defaultCamera;

        void Awake()
        {
            HiddenPlaceCtrl.EnteringHiddenPlace += HiddenPlaceCtrl_EnteringHiddenPlace;
            HiddenPlaceCtrl.ExitingHiddenPlace += HiddenPlaceCtrl_ExitingHiddenPlace;
        }

        void HiddenPlaceCtrl_EnteringHiddenPlace()
        {
            _defaultCamera.SetActive(false);
        }

        void HiddenPlaceCtrl_ExitingHiddenPlace()
        {
            _defaultCamera.SetActive(true);
        }

        void OnDestroy()
        {
            HiddenPlaceCtrl.EnteringHiddenPlace -= HiddenPlaceCtrl_EnteringHiddenPlace;
            HiddenPlaceCtrl.ExitingHiddenPlace -= HiddenPlaceCtrl_ExitingHiddenPlace;
        }
    }
}
