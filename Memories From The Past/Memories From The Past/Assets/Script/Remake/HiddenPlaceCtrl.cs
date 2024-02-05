using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Remake
{
    public sealed class HiddenPlaceCtrl : MonoBehaviour
    {
        public static Action EnteringHiddenPlace;
        public static Action ExitingHiddenPlace;

        private TilemapRenderer _tilemapRenderer;

        [Tooltip("This is the delay to show hidden places")]
        public float DelayCount;

        private void Awake()
        {
            _tilemapRenderer = GetComponent<TilemapRenderer>();
        }

        private IEnumerator DelayToShowHidden()
        {
            OnEnteringHiddenPlace();
            yield return new WaitForSeconds(DelayCount);
            _tilemapRenderer.enabled = false;
        }

        private void OnEnteringHiddenPlace()
        {
            EnteringHiddenPlace?.Invoke();
        }

        private void OnExitingHiddenPlace()
        {
            _tilemapRenderer.enabled = true;
            ExitingHiddenPlace?.Invoke();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(DelayToShowHidden());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                OnExitingHiddenPlace();
            }
        }
    }
}
