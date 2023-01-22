using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicList : MonoBehaviour
{
    public AudioClip Home;
    public AudioClip Outside;
    public AudioClip Dance;
    public AudioClip Jail;
    public AudioClip JailCell;
    public AudioClip PrintingPress;
    public AudioClip Admin;
    public AudioClip Chase;
    public AudioClip Caught;
    public AudioClip HighSusOutside;
    public AudioClip HighSusChase;
    public AudioClip HighSusCaught;
    public AudioClip NoSound;

    public AudioClip GetClip(MusicListEnum audio)
    {
        AudioClip thisClip = NoSound;
        switch(audio)
        {
            case MusicListEnum.Home:
                thisClip = Home;
                break;
            case MusicListEnum.Outside:
                thisClip = Outside;
                break;
            case MusicListEnum.Dance:
                thisClip = Dance;
                break;
            case MusicListEnum.Jail:
                thisClip = Jail;
                break;
            case MusicListEnum.JailCell:
                thisClip = JailCell;
                break;
            case MusicListEnum.PrintingPress:
                thisClip = PrintingPress;
                break;
            case MusicListEnum.Admin:
                thisClip = Admin;
                break;
            case MusicListEnum.Caught:
                thisClip = Caught;
                break;
            case MusicListEnum.Chase:
                thisClip = Chase;
                break;
            case MusicListEnum.HighSusOutside:
                thisClip = HighSusOutside;
                break;
            case MusicListEnum.HighSusChase:
                thisClip = HighSusChase;
                break;
            case MusicListEnum.HighSusCaught:
                thisClip = HighSusCaught;
                break;
            case MusicListEnum.NoSound:
                thisClip = NoSound;
                break;
        }
        return thisClip;
    }

}
