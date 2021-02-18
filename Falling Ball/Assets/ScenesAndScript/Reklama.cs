using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Reklama : MonoBehaviour, IUnityAdsListener
{
    #region Variable
    [SerializeField]
    private Text CoinText;
    #endregion
    #region Dane i zmienne do inicjalizacji
    string GooglePlay_ID = "3943939";
    bool testMode = false;
    //ID zwykłego banera
    string bannerID = "banner";
    //ID pełnoekranowej reklamy z pominięciem po 5 sek.
    string interstitialID = "interstitialVideo";
    //ID reklamy z nagrodami
    string myPlacementId10Gold = "rewardedVideo";
    #endregion

    #region Wyświetlanie konkretnych reklam
    //Standardowa pełnoekranowa reklama do pominięcia
    public void DisplayInterstialAD()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else
        {
            Debug.Log("Reklama nie jest jeszcze gotowa. Proszę spróbuj ponownie później.");
        }
    }
    //Reklama pełnoekranowa z punktami bez możliwości pominięcia
    public void DisplayVideoAD10Gold()
    {
        Advertisement.Show(myPlacementId10Gold);
    }

    //Obsługa pokazania baneru
    //Advertisement.Banner.Hide(); - ukrycie baneru
    public void DisplayBanner()
    {
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        StartCoroutine(ShowBannerWhenInitialized());
    }
    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(bannerID);
    }
    #endregion

    #region Obsługa Nasłuchiwania reklam (czy skończone i jak)
    //Odpowiednia obsługa w zależności od tego jak zakończona reklama
    /// <summary>
    /// Obsługa zakończenia reklamy, którą można pominąć
    /// </summary>
    /// <param name="placementId">Nazwa reklamy jakiej dotyczy obsługa</param>
    /// <param name="showResult">Rezyltat reklamy (oglądnięta, pominięta, czy błąd)
    /// Dotyczy tylko trklam, które można pominać</param>
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            if (placementId == "rewardedVideo")
            {
                GlobalVariable.ActualGold = GlobalVariable.ActualGold + 10;
                if (CoinText !=null) CoinText.text = GlobalVariable.ActualGold.ToString();

            }
            Debug.Log("Reklama oglądnięta. " + placementId);
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Reklama pominięta. " + placementId);
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId10Gold)
        {

            //Debug.Log("Reklama gotowa. " + placementId);
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }
    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
        //Debug.Log("Błąd wczytania reklamy. " + message);
    }
    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    #endregion

    /// <summary>
    /// Funkcja zwiększająca licznik wczytanych i restartowanych plansz.
    /// </summary>
    public void ZwiększCountAds()
    {
        //Pobiera aktualną liczbę śmierci i wczytanych scen
        int licznik = PlayerPrefs.GetInt(GlobalVariable.AdsViverPlayerPrefs, 0);

        if (GlobalVariable.AdcCount == licznik)
        {
            //Wyświetla reklamę
            DisplayInterstialAD();
            //Resetuje licznik
            licznik = 0;
        }
        else
        {
            licznik = licznik + 1;
        }
        //Zapisuje nową wartość do PlayerPrefs
        PlayerPrefs.SetInt(GlobalVariable.AdsViverPlayerPrefs, licznik);
    }

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(GooglePlay_ID, testMode);
        DisplayBanner();
    }
}
