using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;

public class AdsBanner : MonoBehaviour {

    private BannerView banner;

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-9478315162108738/5749599834";
        // Create a 320x50 banner at the top of the screen.
        banner = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        banner.LoadAd(request);
    }

    // Use this for initialization
    void Start () {
        RequestBanner();
        banner.Show();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
