using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;

    public int MaxHp = 10;
    public int hp = 10;

    public int Hp
    {
        set
        {
            hp = value;
            if (hp > 10) hp = 10;
            if (hp < 0) hp = 0;
            PlayerPanelControl.Instance.SetHp(hp);
        }
        get { return hp; }
    }

    public int Attack = 3;

    void Awake()
    {
        Instance = this;
    }

    public void GetHit(int num)
    {
        StateBase.state.GetHit(num);
    }

    public void OnAnimationHit()
    {
        Debug.Log(StateBase.state.GetType().Name);
        StateBase.state?.Hit();
    }
}
