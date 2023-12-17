using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager intance;

    bool mute;

    public static GameManager Instance()
    {
        return intance;
    }

    private void Awake()
    {
        if (intance != null && intance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            intance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        mute = false;
    }

    public bool ToggleMute()
    {
        mute = !mute;

        return mute;
    }

    public bool IsMuted()
    {
        return mute;
    }

}
