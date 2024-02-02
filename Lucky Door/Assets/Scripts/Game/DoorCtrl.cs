using System;
using UnityEngine;

public sealed class DoorCtrl : MonoBehaviour
{
    public static Action<DoorCtrl> DoorOpened;

    [SerializeField] private GameObject _clickText;

    private bool _isMouseInside;

    void Update()
    {
        if(_isMouseInside && Input.GetButton("Fire1"))
        {
            OnDoorOpened();
        }
    }

    void OnDoorOpened()
    {
        DoorOpened?.Invoke(this);
    }

    void OnMouseEnter()
    {
        _clickText.SetActive(true);
        _isMouseInside = true;
    }

    void OnMouseExit()
    {
        _clickText.SetActive(false);
        _isMouseInside = false;
    }
}
