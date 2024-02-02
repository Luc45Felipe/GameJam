using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Status status;
    HudCtrl hudCtrl;

    void Awake()
    {
        hudCtrl = GetComponent<HudCtrl>();
        status = GetComponent<Status>();
        hudCtrl.UpdateUI(status.Life);
    }

    public void EndStage()
    {
        SceneCtrl.NextScene();
    }

    public void Replay()
    {
        SceneCtrl.Replay();
    }

    public void GetDamage()
    {
        if(status.GetDamage())
        {
            Replay();
        }
        else
        {
            hudCtrl.UpdateUI(status.Life);    
        }
    }
}
