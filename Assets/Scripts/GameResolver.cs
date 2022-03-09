using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResolver : MonoBehaviour
{

    private bool _didSeekerWin = false;
    private bool _didHiderWin = false;

    /// <summary>
    /// Open UI if won
    /// Restard game
    /// Disconnect
    /// </summary>

    public bool didSeekerWin
    {
        get { return _didSeekerWin; }
        set { _didSeekerWin = value; }
    }

    public bool didHiderWin
    {
        get { return _didHiderWin; }
        set { _didHiderWin = value; }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_didSeekerWin)
        {

        }else if (_didHiderWin)
        {

        }
    }
}
