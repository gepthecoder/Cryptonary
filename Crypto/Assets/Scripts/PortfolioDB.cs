using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortfolioDB : MonoBehaviour
{
    public List<Portfolio> USER_PORTFOLIOS = new List<Portfolio>();

    #region Singleton
    private static PortfolioDB instance;
    public static PortfolioDB Instance
    {
        get
        {
            if (instance == null) { instance = FindObjectOfType(typeof(PortfolioDB)) as PortfolioDB; }
            return instance;
        }
        set { instance = value; }
    }
    #endregion

    void Awake() { instance = this; }

    void Start()
    {
        if(SaveManager.instance.state.hasPortfolio == true)
        {
            foreach(Portfolio item in SaveManager.instance.state.portfolioList)
            {
                USER_PORTFOLIOS.Add(item);
            }
        }
    }


}
