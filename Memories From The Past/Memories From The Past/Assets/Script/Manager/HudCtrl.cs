using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudCtrl : MonoBehaviour
{
    [Header("Life")]
    [SerializeField]private Sprite empty;
    [SerializeField]private Sprite full;
    [SerializeField]private List<Image> hearts;

    public void PauseBtn()
    {
        SceneCtrl.Pause();
    }

    public void UpdateUI(int life)
    {
        RefreshLife(life);
    }

    public void RefreshLife(int life)
    {
        switch (life)
        {
            case 3:
                foreach (Image heart in hearts)
                {
                    heart.sprite = full;
                }
                break;

            case 2:
                hearts[2].sprite = empty;
                break;

            case 1:
                hearts[1].sprite = empty;
                break;

            case 0:
                foreach (Image heart in hearts)
                {
                    heart.sprite = empty;
                }
                break;
        }
    }
}
