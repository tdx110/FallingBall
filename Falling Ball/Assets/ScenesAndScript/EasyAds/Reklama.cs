using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reklama : MonoBehaviour
{
    private const int CountAds = 4;

    [SerializeField]
    private EasyAds easyAds;

    private static int count = 0;
    // Start is called before the first frame update

    private void Start()
    {
        //ShowBanner();
    }
    public void ShowBanner()
    {
        easyAds.ShowBanner();
    }
    public void IncCount()
    {
        count++;

        if (count == CountAds)
        {
#if COUNT_ADS
#else
            easyAds.RequestInterstitial();
            count = 0;
#endif
        }

    }
}


