using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AppHandler : MonoBehaviour
{

    #region Singleton
    private static AppHandler instance;
    public static AppHandler Instance
    {
        get
        {
            if (instance == null) { instance = FindObjectOfType(typeof(AppHandler)) as AppHandler; }
            return instance;
        }
        set { instance = value; }
    }
    #endregion

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(SaveManager.instance.state.favouriteList != null)
        {
            for (int i = 0; i < SaveManager.instance.state.favouriteList.Length; i++)
            {
                string newItem = SaveManager.instance.state.favouriteList[i];
                FAVOURITE_CRYPTOS.Add(newItem);
            }
        }
    }

    public Sprite emptyStar;
    public Sprite filledStar;

    public List<string> FAVOURITE_CRYPTOS;

    /// <summary>
    /// Array of app features
    /// </summary>
    /// <returns>MAIN -> 0, FAVOURITES, PORTFOLIO, GLOBAL DATA, NEWS, MORE</returns>
    public GameObject[] CONTENT;

    #region MAIN CONTENT BUTTON HANDLERS
    public void OpenMarket()
    {
        foreach(GameObject feature in CONTENT)
        {
            if(feature.name == "MAIN")
            {
                CryptoManager.Instance.dictOfGains.Clear();
                feature.SetActive(true);
                //TO:DO -> Load Items From Market v/
                StartCoroutine(CryptoManager.Instance.LoadItems());
            }
            else { feature.SetActive(false); }
        }
    }

    public void OpenFavorites()
    {
        foreach (GameObject feature in CONTENT)
        {
            if (feature.name == "FAVOURITES")
            {
                feature.SetActive(true);
                //TO:DO -> Load Items From Favourites
                StartCoroutine(CryptoManager.Instance.LoadFavouriteItems());
            }
            else { feature.SetActive(false); }
        }
    }

    public void OpenPortfolio()
    {
        foreach (GameObject feature in CONTENT)
        {
            if (feature.name == "PORTFOLIO")
            {
                feature.SetActive(true);
                //TO:DO -> Load Portfolio UI
            }
            else { feature.SetActive(false); }
        }
    }

    public void OpenGlobalData()
    {
        foreach (GameObject feature in CONTENT)
        {
            if (feature.name == "GLOBAL_DATA")
            {
                feature.SetActive(true);
                //TO:DO -> Load Items From Global Data
            }
            else { feature.SetActive(false); }
        }
    }

    public void OpenNews()
    {
        foreach (GameObject feature in CONTENT)
        {
            if (feature.name == "NEWS")
            {
                feature.SetActive(true);
                //TO:DO -> Load Items From News
            }
            else { feature.SetActive(false); }
        }
    }

    public void OpenMoreFeatures()
    {
        foreach (GameObject feature in CONTENT)
        {
            if (feature.name == "MENU")
            {
                feature.SetActive(true);
            }
            else { feature.SetActive(false); }
        }
    }
    #endregion

    #region MORE FEATURES

    #region SignIn/SignUp
    public GameObject[] LoginTemplateObjs;

    public void OpenLoginBanner()
    {// banner for user to sign in or sign up
        foreach (GameObject template in LoginTemplateObjs)
        {
            if(template.name == "USER-LOGIN-REGISTER-BANNER")
            {
                template.SetActive(true);
            }
            else
            {
                template.SetActive(false);
            }
        }
    }

    public void CancelLoginBanner()
    {
        foreach (GameObject template in LoginTemplateObjs)
        {
            template.SetActive(false);
        }
    }

    public void OpenCreateAccount()
    {
        foreach (GameObject template in LoginTemplateObjs)
        {
            if(template.name == "CREATE-ACCOUNT")
            {
                template.SetActive(true);
            }
        }
    }

    public void CloseCreateAccount()
    {
        foreach (GameObject template in LoginTemplateObjs)
        {
            if (template.name == "CREATE-ACCOUNT")
            {
                template.SetActive(false);
            }
        }
    }

    public void OpenSignIn()
    {
        foreach (GameObject template in LoginTemplateObjs)
        {
            if (template.name == "SIGN-IN")
            {
                template.SetActive(true);
            }
        }
    }

    public void CloseSignIn()
    {
        foreach (GameObject template in LoginTemplateObjs)
        {
            if (template.name == "SIGN-IN")
            {
                template.SetActive(false);
            }
        }
    }
    #endregion

    #endregion

}
