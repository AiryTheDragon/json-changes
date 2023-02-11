using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMusicList : MonoBehaviour
{
    public AudioClip WhiskedAwayEnding;
    public AudioClip Isolation;
    public AudioClip Executed;
    public AudioClip BatteredButFree;
    public AudioClip Victorious;
    public AudioClip SilverTongue;
    public AudioClip Dreams;
    public AudioClip NoSound;
    public AudioClip Other;

    public AudioClip GetClip(EndGameMusicListEnum audio)
    {
        AudioClip thisClip = NoSound;
        switch(audio)
        {
            case EndGameMusicListEnum.WhiskedAway:
                thisClip = WhiskedAwayEnding;
                break;
            case EndGameMusicListEnum.Isolation:
                thisClip = Isolation;
                break;
            case EndGameMusicListEnum.Executed:
                thisClip = Executed;
                break;
            case EndGameMusicListEnum.BatteredButFree:
                thisClip = BatteredButFree;
                break;
            case EndGameMusicListEnum.Victorious:
                thisClip = Victorious;
                break;
            case EndGameMusicListEnum.Dreams:
                thisClip = Dreams;
                break;
            case EndGameMusicListEnum.NoSound:
                thisClip = NoSound;
                break;
            case EndGameMusicListEnum.Other:
                thisClip = Other;
                break;
        }
        return thisClip;
    }

}
