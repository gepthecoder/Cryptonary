using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cryptocurrency;
using System.Globalization;
using System.Linq;

public class CryptoManager : MonoBehaviour
{

    #region Singleton
    private static CryptoManager instance;
    public static CryptoManager Instance
    {
        get
        {
            if (instance == null) { instance = FindObjectOfType(typeof(CryptoManager)) as CryptoManager; }
            return instance;
        }
        set { instance = value; }
    }
    #endregion
    /// <summary>
    /// the Array of identifier name symbols.
    /// </summary>
    string[,] IdNameSymbol;

    #region MAIN
    /// <summary>
    /// The template object
    /// </summary>
    public GameObject template;

    /// <summary>
    /// The content of the scroll view
    /// </summary>
    public GameObject content;

    /// <summary>
    /// NumberOfCryptoCurrencies
    /// </summary>
    [SerializeField]
    private int numOfCurrencies = 3;
    #endregion

    #region TOP GAINERS
    /// <summary>
    /// The template object - TOP GAINERS
    /// </summary>
    public GameObject templateGains;

    /// <summary>
    /// The content of the scroll view - TOP GAINERS
    /// </summary>
    public GameObject contentGains;

    /// <summary>
    /// NumberOfCryptoCurrencies - TOP Gainers
    /// </summary>
    [SerializeField]
    private int numOfTopGainers = 5;
    #endregion

    #region FAVOURITES
    /// <summary>
    /// The template object
    /// </summary>
    public GameObject templateFavourite;

    /// <summary>
    /// The content of the scroll view
    /// </summary>
    public GameObject contentFavourites;

    #endregion

    #region SEARCH
    public GameObject SEARCH_PORTFOLIO_TEMPLATE;
    public GameObject SEARCH_CONTENT;
    public int numOfSearchItems = 50;
    #endregion

    /// <summary>
    /// The load status text
    /// </summary>
    public Text loadStatus;

    /// <summary>
    /// The index as we loop though the array
    /// </summary>
    private int x = 0;
    private int y = 0;
    private int z = 0;



    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //get the array 
        IdNameSymbol = Cryptocurrencies.getIdNameSymbols();

        loadStatus.text = "0%";

        //StartCoroutine("LoadPortfolioSearchItems");
        //start loading items
        StartCoroutine("LoadItems");

        //this will cause an error!
        //print(Cryptocurrencies.getPrice("this"));
    }

    private void KILL_CHILDREN()
    {
        //clear children
        int numOfChildren = SEARCH_CONTENT.transform.childCount;
        for (int i = 0; i < numOfChildren; i++)
        {
            GameObject child = SEARCH_CONTENT.transform.GetChild(i).gameObject;
            Destroy(child);
        }
        Debug.Log("Children deleted..");
    }

    public void SEARCH_ITEMS(string input)
    {
        KILL_CHILDREN();
        StartCoroutine(LoadPortfolioSearchItemsRealTime(input));
    }

    public void LOAD_SEARCH_ITEMS_ALL()
    {
        KILL_CHILDREN();
        StartCoroutine(LoadPortfolioSearchItems());
    }

    public IEnumerator LoadPortfolioSearchItemsRealTime(string input)
    {
        //load items that corespond to searched string
        while (z < numOfSearchItems)
        {
            //get strings
            string id = IdNameSymbol[z, 0];
            string name = IdNameSymbol[z, 1];
            string symbol = IdNameSymbol[z, 2];

            if (!symbol.Contains(input.ToLower()) || !name.Contains(input.ToLower())
                || !symbol.Contains(input.ToUpper()) || !symbol.Contains(input.ToUpper())
                )
            {
                z++;
                yield return null;
            }
            else
            {
                //get the icon
                Sprite icon = Cryptocurrencies.getIcon(symbol, Cryptocurrencies.iconTypes.color);

                // check for nullable icon
                if (icon == null)
                {
                    icon = Resources.Load("unknown", typeof(Sprite)) as Sprite;
                }

                //create the new item...and set the data
                GameObject newItem = Instantiate(SEARCH_PORTFOLIO_TEMPLATE);
                newItem.transform.Find("icon").GetComponent<Image>().sprite = icon;
                newItem.transform.Find("Name").GetComponent<Text>().text = name;
                newItem.transform.Find("Symbol").GetComponent<Text>().text = symbol;

                //move the parent object and resize
                newItem.transform.SetParent(SEARCH_CONTENT.transform);
                newItem.transform.localScale = new Vector3(2f, 2f, 2f);
                //increment
                z++;

                //rename go
                newItem.transform.name = symbol;
                yield return null;
                //yield return new WaitForSeconds(0.07f);
            }
        }
        //done!
        z = 0;
    }
    /// <summary>
    /// Loads the search items.
    /// </summary>
    /// <returns>Search items.</returns>
    public IEnumerator LoadPortfolioSearchItems()
    {
        while (y < numOfSearchItems)
        {
            //get strings
            string id = IdNameSymbol[y, 0];
            string name = IdNameSymbol[y, 1];
            string symbol = IdNameSymbol[y, 2];

            //get the icon
            Sprite icon = Cryptocurrencies.getIcon(symbol, Cryptocurrencies.iconTypes.color);

            // check for nullable icon
            if (icon == null)
            {
                icon = Resources.Load("unknown", typeof(Sprite)) as Sprite;
            }

            //create the new item...and set the data
            GameObject newItem = Instantiate(SEARCH_PORTFOLIO_TEMPLATE);
            newItem.transform.Find("icon").GetComponent<Image>().sprite = icon;
            newItem.transform.Find("Name").GetComponent<Text>().text = name;
            newItem.transform.Find("Symbol").GetComponent<Text>().text = symbol;

            //move the parent object and resize
            newItem.transform.SetParent(SEARCH_CONTENT.transform);
            newItem.transform.localScale = new Vector3(2f, 2f, 2f);
            //increment
            y++;

            //rename go
            newItem.transform.name = symbol;

            yield return null;
            //yield return new WaitForSeconds(0.07f);
        }
        //done!
        y = 0;
    }


    /// <summary>
    /// Loads the items.
    /// </summary>
    /// <returns>The items.</returns>
    public IEnumerator LoadItems()
    {
        while (x < numOfCurrencies)
        {
            //get strings
            string id = IdNameSymbol[x, 0];
            string name = IdNameSymbol[x, 1];
            string symbol = IdNameSymbol[x, 2];

            //get the icon
            Sprite icon = Cryptocurrencies.getIcon(symbol, Cryptocurrencies.iconTypes.color);

            // check for nullable icon
            if (icon == null)
            {
                icon = Resources.Load("unknown", typeof(Sprite)) as Sprite;
            }

            //log
            print(id + ", " + name + ", " + symbol);

            //create the new item...and set the data
            GameObject newItem = Instantiate(template);
            newItem.transform.Find("icon").GetComponent<Image>().sprite = icon;
            newItem.transform.Find("NameSymbol/Name").GetComponent<Text>().text = name;
            newItem.transform.Find("NameSymbol/Symbol").GetComponent<Text>().text = "("+symbol+")";

            float[] percentages = Cryptocurrencies.getPercentChangedAll(symbol);
            newItem.transform.Find("NameSymbol/Percents/1H").GetComponent<Text>().text  = "1h\n"  + percentages[0].ToString().Substring(0,4) + "%";
            newItem.transform.Find("NameSymbol/Percents/24H").GetComponent<Text>().text = "24h\n" + percentages[1].ToString().Substring(0, 4) + "%";
            newItem.transform.Find("NameSymbol/Percents/7D").GetComponent<Text>().text  = "7d\n" + percentages[2].ToString().Substring(0, 4) + "%";

            newItem.transform.Find("Price").GetComponent<Text>().text = Cryptocurrencies.getPrice(symbol).ToString() + "$";

            newItem.transform.Find("VolumeCap/Volume").GetComponent<Text>().text = ConvertNumber((decimal)Cryptocurrencies.getVolume(symbol));
            newItem.transform.Find("VolumeCap/MarketCap").GetComponent<Text>().text = ConvertNumber((decimal)Cryptocurrencies.getMarketCap(symbol));

            newItem.transform.Find("icon/Rank").GetComponent<Text>().text = Cryptocurrencies.getRank(symbol).ToString();


            //move the parent object and resize
            newItem.transform.SetParent(content.transform);
            newItem.transform.localScale = new Vector3(2f, 2f, 2f);

            //increment
            x++;

            //update loadstatus
            loadStatus.text = ((x / (IdNameSymbol.Length / 3f)) * 100f).ToString() + "%";

            //update dict
             dictOfGains.Add(symbol, percentages[1]);

            //rename go
            newItem.transform.name = symbol;


            yield return null;
            //yield return new WaitForSeconds(0.07f);
        }

        //finish
        template.SetActive(false);
        loadStatus.text = "100%";
        //done!
        StartCoroutine(LoadTopGainers());
        SetStarImages();
    }


    public Dictionary<string, float> dictOfGains = new Dictionary<string, float>();

    /// <summary>
    /// Loads the top gainers.
    /// </summary>
    /// <returns>The items.</returns>
    private IEnumerator LoadTopGainers()
    {
        //get top x gainers -> sort; take first x gainers
        var ordered = dictOfGains.OrderBy(y => y.Value).ToDictionary(x => x.Key, y => y.Value);
        var selectedDict = ordered.Take(numOfTopGainers);
        Debug.Log(selectedDict.Count());

        foreach(var item in selectedDict)
        {
            string symbol = item.Key;
            float gain = item.Value;
            //get the icon
            Sprite icon = Cryptocurrencies.getIcon(symbol, Cryptocurrencies.iconTypes.color);

            // check for nullable icon
            if (icon == null)
            {
                icon = Resources.Load("unknown", typeof(Sprite)) as Sprite;
            }

            GameObject newItem = Instantiate(templateGains);
            newItem.transform.Find("icon").GetComponent<Image>().sprite = icon;
            newItem.transform.Find("Symbol&Gain24/Symbol").GetComponent<Text>().text = symbol.ToUpper();
            newItem.transform.Find("Symbol&Gain24/24H").GetComponent<Text>().text = gain.ToString();

            //move the parent object and resize
            newItem.transform.SetParent(contentGains.transform);
            newItem.transform.localScale = new Vector3(2f, 2f, 2f);

            yield return null;
        }
    }

    public string ConvertNumber(decimal num)
    {
        if (num > 999999999 || num < -999999999)
        {
            return num.ToString("0,,,.###B", CultureInfo.InvariantCulture);
        }
        else
        if (num > 999999 || num < -999999)
        {
            return num.ToString("0,,.##M", CultureInfo.InvariantCulture);
        }
        else
        if (num > 999 || num < -999)
        {
            return num.ToString("0,.#K", CultureInfo.InvariantCulture);
        }
        else
        {
            return num.ToString(CultureInfo.InvariantCulture);
        }
    }

    [SerializeField]
    private Text LoadFave_NotificationError;

    /// <summary>
    /// Loads the Favourite items.
    /// </summary>
    /// <returns>The Fave items.</returns>
    public IEnumerator LoadFavouriteItems()
    {
        if (AppHandler.Instance.FAVOURITE_CRYPTOS.Count /*SaveManager.instance.state.favouriteList*/ == 0)
        {
            // display text to user
            LoadFave_NotificationError.text = "To populate this area select the star on your favourite crypto.";
            yield return null;
        }
        else
        {
            LoadFave_NotificationError.text = "";
        }

        // clear children
        for (int i = 0; i < contentFavourites.transform.childCount; i++)
        {
            GameObject temp = contentFavourites.transform.GetChild(i).gameObject;
            Destroy(temp);
        }

        Debug.Log("Loading cryptos.. lenght: " + AppHandler.Instance.FAVOURITE_CRYPTOS.Count);

        foreach (string crypto in /*SaveManager.instance.state.favouriteList*/AppHandler.Instance.FAVOURITE_CRYPTOS)
        {
            //get strings
            string symbol = crypto;

            //get the icon
            Sprite icon = Cryptocurrencies.getIcon(symbol, Cryptocurrencies.iconTypes.color);

            // check for nullable icon
            if (icon == null)
            {
                icon = Resources.Load("unknown", typeof(Sprite)) as Sprite;
            }

            //create the new item...and set the data
            GameObject newItem = Instantiate(templateFavourite);
            newItem.transform.Find("icon").GetComponent<Image>().sprite = icon;
            newItem.transform.Find("NameSymbol/Symbol").GetComponent<Text>().text = "(" + symbol + ")";

            float[] percentages = Cryptocurrencies.getPercentChangedAll(symbol);
            newItem.transform.Find("Percents/1H").GetComponent<Text>().text = "1h\n" + percentages[0].ToString().Substring(0, 4) + "%";
            newItem.transform.Find("Percents/24H").GetComponent<Text>().text = "24h\n" + percentages[1].ToString().Substring(0, 4) + "%";
            newItem.transform.Find("Percents/7D").GetComponent<Text>().text = "7d\n" + percentages[2].ToString().Substring(0, 4) + "%";

            newItem.transform.Find("Price").GetComponent<Text>().text = Cryptocurrencies.getPrice(symbol).ToString() + "$";
            newItem.transform.Find("icon/Rank").GetComponent<Text>().text = Cryptocurrencies.getRank(symbol).ToString();

            //move the parent object and resize
            newItem.transform.SetParent(contentFavourites.transform);
            newItem.transform.localScale = new Vector3(2f, 2f, 2f);

            //increment
            x++;

            //update loadstatus
            loadStatus.text = ((x / (IdNameSymbol.Length / 3f)) * 100f).ToString() + "%";

            //rename go
            newItem.transform.name = symbol;

            yield return null;
            //yield return new WaitForSeconds(0.07f);
        }

        //finish
        //templateFavourite.SetActive(false);
        loadStatus.text = "100%";
        //done!
        Debug.Log("All cryptos are loaded..");
    }

    public void SetStarImages()
    {
        GameObject[] cryptocurrencies = GameObject.FindGameObjectsWithTag("CRYPTO");
        Debug.Log("lenght: " + cryptocurrencies.Length);
        foreach (GameObject crypto in cryptocurrencies)
        {
            string symbol = crypto.name;
            Debug.Log("current symbol: " + symbol);
            foreach (string fave_crypto in AppHandler.Instance.FAVOURITE_CRYPTOS)
            {
                if(fave_crypto == symbol)
                {
                    Image s = crypto.transform.Find("Price/Star").GetComponent<Image>();
                    if(s != null)
                    {
                        s.sprite = AppHandler.Instance.filledStar;
                        Debug.Log("filled: " + s.gameObject.name);
                    }
                }             
            }
        }
    }


}
