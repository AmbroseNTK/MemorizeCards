using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;

public class AdsInterstitial : MonoBehaviour {


    InterstitialAd interstitial;

    private void RequestInterstitial()
    {
        MobileAds.Initialize("ca-app-pub-9478315162108738~2920681547");
        string adUnitId = "ca-app-pub-9478315162108738/4972129819";

        // Initialize an InterstitialAd.
        InterstitialAd interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);

    }

    // Use this for initialization
    void Start () {
        RequestInterstitial();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ShowAds()
    {
        Debug.Log("Show Ads");
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
}
