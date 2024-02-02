using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    private int life = 3;
    public int Life { get { return life; } }

    public bool GetDamage()
    {
        life--;
        
        if(life<= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
