using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ADScript : MonoBehaviour
{
    private const float CheckTimer = 150f;
    private const float TimeOffset = 5f;

    public string nameScene;
    private string lastIsAdsOpen = null;
    private float timer;

    public Text adWarningScene;
    //public TextMeshProUGUI adWarningSetting;
    //public TextMeshProUGUI adWarningCategory;

    public void ShareFriend(){
#if UNITY_WEBGL && !UNITY_EDITOR
        WebGLPluginJS.ShareFunction();
#endif
    }

    public void ShowAdInterstitial(){
        if (timer > CheckTimer)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
    	            WebGLPluginJS.InterstitialFunction();
#endif
            timer = 0f;
            StartCoroutine(Courutine(1));
        }
    }

    IEnumerator Courutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        adWarningScene.text = "Кликни по экрану";
        //adWarningSetting.gameObject.SetActive(false);
        //adWarningCategory.gameObject.SetActive(false);
    }

    public void ShowAdReward(){
#if UNITY_WEBGL && !UNITY_EDITOR
    	WebGLPluginJS.RewardFunction();
#endif
       // sliderHome.value += rewardBonusSliderHome;
    	//if(sliderFuelCar.value<=lowBalanceFuel) sliderFuelCar.value += rewardBonusSliderFuel;
    }

    private void Start()
    {
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        timer += deltaTime;

        CheckAds();
        if (timer + TimeOffset > CheckTimer)
        {
            adWarningScene.gameObject.SetActive(true);
           // adWarningSetting.gameObject.SetActive(true);
            //adWarningCategory.gameObject.SetActive(true);
            if (timer > CheckTimer)
            {
                ShowAdInterstitial();
            }
            else
            {
                var timeRest = Math.Floor(CheckTimer - timer) + 1;
                adWarningScene.text = $"Реклама через: {timeRest}";
               // adWarningSetting.text = $"Реклама через: {timeRest}";
               // adWarningCategory.text = $"Реклама через: {timeRest}";
            }
        }    
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
        }
    }

    public void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
        }
    }

    public void AdsClosed()
    {
        timer = 0f;
    }

    public void CheckAds()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        var adsOpen = WebGLPluginJS.GetAdsOpen();
        if (lastIsAdsOpen == null) {
            lastIsAdsOpen = adsOpen;
        }

        if (adsOpen == "yes")
        {
            AudioListener.pause = true;
            lastIsAdsOpen = "yes";
        }
        else
        {
            //Коничлась реклама
            AudioListener.pause = false;
            if (lastIsAdsOpen == "yes") {
                AdsClosed();
                lastIsAdsOpen = "no";
            }
        }
#endif
    }
}
