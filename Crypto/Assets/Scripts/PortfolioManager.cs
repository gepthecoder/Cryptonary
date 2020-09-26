using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class PortfolioManager : MonoBehaviour
{
    public GameObject ADD_TRANSACTION_BANNER;
    public GameObject NEW_PORTFOLIO_BANNER;
    public GameObject MAIN_PORTFOLIO;
    public GameObject SEARCH_ENGINE;

    // NEW PORTFOLIO
    public InputField InputField_MyPortfolio_Name;
    public Button BTN_CreatePortfolio;
    public Animator ANIME_NewPortfolioTemplate;
    //

    //GET STARTED / CREATE TRANSACTION PAGE
    public Text main_message;
    public Text sub_message;

    public Button BTN_GET_STARTED;
    public Button BTN_ADD_TRANSACTION;
    //



    void Start()
    {
        if (SaveManager.instance.state.hasPortfolio)
        {
            // show add transaction banner on main portfolio page
            PORTFOLIO_CONTENT_TWO();
        }
        else
        {
            // default
            PORTFOLIO_CONTENT_0NE();
        }

    }
    
    public void CREATE_PORTFOLIO()
    {
        int id = Random.Range(0, 1000);
        string portfolioName = InputField_MyPortfolio_Name.text;
        if (portfolioName != "")
        {
            Portfolio item = new Portfolio(
                id, portfolioName
                );

            PortfolioDB.Instance.USER_PORTFOLIOS.Add(item);
            SAVE_TO_DB(PortfolioDB.Instance.USER_PORTFOLIOS);
            //FileStream fs = new FileStream("save.dat", )


            SaveManager.instance.state.hasPortfolio = true;
            SaveManager.instance.Save();

            ANIME_NewPortfolioTemplate.SetTrigger("success");
            BTN_CreatePortfolio.interactable = false;
            Invoke("PORTFOLIO_CREATED_SUCCESSFULLY", 1f);
        }
    }

    public void PORTFOLIO_CREATED_SUCCESSFULLY()
    {
        ClearNewPortfolioField();
        BackToMainPortfolioBanner();
        PORTFOLIO_CONTENT_TWO();
    }

    public void REMOVE_PORTFOLIO_FROM_LIST(int id)
    {
        foreach (Portfolio portfolio in PortfolioDB.Instance.USER_PORTFOLIOS)
        {
            if(portfolio.id == id)
            {
                PortfolioDB.Instance.USER_PORTFOLIOS
                    .Remove(portfolio);
                print("portfolio removed from list..\nSaving to DB..");
                SAVE_TO_DB(PortfolioDB.Instance.USER_PORTFOLIOS);
                break;
            }
        }
    }

    private void SAVE_TO_DB(List<Portfolio> portfolios)
    {
        SaveManager.instance.state.portfolioList = portfolios.ToArray();
        SaveManager.instance.Save();
    }


    public void BackToMainPortfolioBanner()
    {
        NEW_PORTFOLIO_BANNER.SetActive(false);
        MAIN_PORTFOLIO.SetActive(true);
    }
    public void ClearNewPortfolioField()
    {
        InputField_MyPortfolio_Name.text = "";
        BTN_CreatePortfolio.interactable = true;
    }

    private void PORTFOLIO_CONTENT_0NE()
    {
        main_message.text = Constants.PORTFOLIO_TITLE_NEW_PORTFOLIO;
        sub_message.text = Constants.PORTFOLIO_SUBTITLE_NEW_PORTFOLIO;
        BTN_GET_STARTED.gameObject.SetActive(true);
        BTN_ADD_TRANSACTION.gameObject.SetActive(false);
    }

    private void PORTFOLIO_CONTENT_TWO()
    {
        main_message.text = Constants.PORTFOLIO_SUBTITLE_NEW_TRANSACTION;
        sub_message.text = Constants.PORTFOLIO_SUBTITLE_NEW_TRANSACTION;
        BTN_ADD_TRANSACTION.gameObject.SetActive(true);
        BTN_GET_STARTED.gameObject.SetActive(false);
    }

    public void GET_STARTED()
    {
        NEW_PORTFOLIO_BANNER.SetActive(true);
    }

    // TRANSACTIONS

    public void ADD_TRANSACTION_OPEN_BANNER()
    {
        ADD_TRANSACTION_BANNER.SetActive(true);
    }

    public void CLOSE_TRANSACTION_BANNER()
    {
        ADD_TRANSACTION_BANNER.SetActive(false);
    }

    public void ADD_MANUALLY()
    {
        //OPEN & LOAD SEARCH ENGINE
        StartCoroutine(CryptoManager.Instance.LoadPortfolioSearchItems());
        SEARCH_ENGINE.SetActive(true);
    }

    public void CLOSE_SEARCH_ENGINE()
    {
        SEARCH_ENGINE.SetActive(false);
    }
}
