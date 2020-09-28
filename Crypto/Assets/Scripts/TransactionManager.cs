using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransactionManager : MonoBehaviour
{
    #region Singleton
    private static TransactionManager instance;
    public static TransactionManager Instance
    {
        get
        {
            if (instance == null) { instance = FindObjectOfType(typeof(TransactionManager)) as TransactionManager; }
            return instance;
        }
        set { instance = value; }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        transaction_number = SaveManager.instance.state.portfolioList[0].transactions.Length;
    }

    #endregion

    public GameObject ADD_SAVE_TRANSACTION_BANNER;

    public GameObject BUY_CONTENT;
    public GameObject SELL_CONTENT;
    public GameObject WATCH_CONTENT;

    public Button BUY_BTN;
    public Button SELL_BTN;
    public Button WATCH_BTN;

    public Sprite BLUE_BTN_IMG;
    public Sprite RED_BTN_IMG;
    public Sprite GREEN_BTN_IMG;
    public Sprite GRAY_BTN_IMG;

    [Header("BUY FIELDS")]
    public Text Btext_Exchange;
    public Text Btext_TradingPair;
    public InputField Btext_PriceUSD;
    public InputField Btext_Quantity;
    public Text Btext_TotalCost;
    public Text Btext_DateTime;
    public InputField Btext_Fee;
    public InputField Btext_Note;

    public void APPEND_BUY_FIELDS(Transaction transaction)
    {
        Btext_Exchange.text = transaction.exchange;
        Btext_TradingPair.text = transaction.trading_pair;
        Btext_PriceUSD.text = transaction.price.ToString();
        Btext_Quantity.text = transaction.quantity.ToString();
        Btext_TotalCost.text = transaction.total_cost.ToString();
        Btext_DateTime.text = transaction.date_time.ToShortDateString();
        Btext_Fee.text = transaction.fee.ToString();
        Btext_Note.text = transaction.note;
    }

    [Header("SELL FIELDS")]
    public Text Stext_Exchange;
    public Text Stext_TradingPair;
    public InputField Stext_PriceUSD;
    public InputField Stext_Quantity;
    public Text Stext_TotalCost;
    public Text Stext_DateTime;
    public InputField Stext_Fee;
    public InputField Stext_Note;

    public void APPEND_SELL_FIELDS(Transaction transaction)
    {
        Stext_Exchange.text = transaction.exchange;
        Stext_TradingPair.text = transaction.trading_pair;
        Stext_PriceUSD.text = transaction.price.ToString();
        Stext_Quantity.text = transaction.quantity.ToString();
        Stext_TotalCost.text = transaction.total_cost.ToString();
        Stext_DateTime.text = transaction.date_time.ToShortDateString();
        Stext_Fee.text = transaction.fee.ToString();
        Stext_Note.text = transaction.note;
    }

    [Header("WATCH FIELDS")]
    public InputField Wtext_Quantity;

    private int currentTypeOfTransaction;

    private Transaction watchTransactionData;

    private int transaction_number;

    public void APPEND_WATCH_FIELD(Transaction transaction)
    {
        Wtext_Quantity.text = transaction.quantity.ToString();  
    }

    public void BUY_OPTION()
    {
        BUY_CONTENT.SetActive(true);
        SELL_CONTENT.SetActive(false);
        WATCH_CONTENT.SetActive(false);

        BUY_BTN.GetComponent<Image>().sprite = GREEN_BTN_IMG;
        SELL_BTN.GetComponent<Image>().sprite = GRAY_BTN_IMG;
        WATCH_BTN.GetComponent<Image>().sprite = GRAY_BTN_IMG;
        currentTypeOfTransaction = (int)TransactionType.BUY;
    }

    public void SELL_OPTION()
    {
        SELL_CONTENT.SetActive(true);
        BUY_CONTENT.SetActive(false);
        WATCH_CONTENT.SetActive(false);

        SELL_BTN.GetComponent<Image>().sprite = RED_BTN_IMG;
        BUY_BTN.GetComponent<Image>().sprite = GRAY_BTN_IMG;
        WATCH_BTN.GetComponent<Image>().sprite = GRAY_BTN_IMG;
        currentTypeOfTransaction = (int)TransactionType.SELL;
    }

    public void WATCH_OPTION()
    {
        WATCH_CONTENT.SetActive(true);
        SELL_CONTENT.SetActive(false);
        BUY_CONTENT.SetActive(false);

        WATCH_BTN.GetComponent<Image>().sprite = BLUE_BTN_IMG;
        BUY_BTN.GetComponent<Image>().sprite = GRAY_BTN_IMG;
        SELL_BTN.GetComponent<Image>().sprite = GRAY_BTN_IMG;

        currentTypeOfTransaction = (int)TransactionType.WATCH;

    }

    public void SetTransactionValues(string id, TransactionType type)
    {
        Transaction newTransaction = new Transaction();
        float price = Cryptocurrency.Cryptocurrencies.getPrice(id);

        switch (type)
        {
            case TransactionType.BUY:
                // CREATE NEW TRANSACTION
                newTransaction.type = TransactionType.BUY;
                newTransaction.exchange = "Global Average";
                newTransaction.trading_pair = id + "/USD";
                newTransaction.price = price;
                float Bquantity = 0;
                newTransaction.quantity = Bquantity;
                bool BzeroQuantity = Bquantity == 0;
                newTransaction.total_cost = BzeroQuantity ? price : price * Bquantity;
                DateTime Btime = DateTime.Now;
                newTransaction.date_time = Btime;
                float Bfee = 0;
                newTransaction.fee = Bfee;
                newTransaction.note = "";
                //
                // APPEND DATA TO FIELDS
                APPEND_BUY_FIELDS(newTransaction);
                break;
            case TransactionType.SELL:
                // CREATE NEW TRANSACTION
                newTransaction.type = TransactionType.SELL;
                newTransaction.exchange = "Global Average";
                newTransaction.trading_pair = id + "/USD";
                newTransaction.price = price;
                float Squantity = 0;
                newTransaction.quantity = Squantity;
                bool SzeroQuantity = Squantity == 0;
                newTransaction.total_cost = SzeroQuantity ? price : price * Squantity;
                DateTime Stime = DateTime.Now;
                newTransaction.date_time = Stime;
                float Sfee = 0;
                newTransaction.fee = Sfee;
                newTransaction.note = "";
                //
                // APPEND DATA TO FIELDS
                APPEND_SELL_FIELDS(newTransaction);
                break;

            case TransactionType.WATCH:
                // CREATE NEW TRANSACTION
                newTransaction.type = TransactionType.WATCH;
                float Wquantity = 0;
                newTransaction.quantity = Wquantity;

                newTransaction.exchange = "Global Average";
                newTransaction.trading_pair = id + "/USD";
                newTransaction.price = price;
                bool WzeroQuantity = Wquantity == 0;
                newTransaction.total_cost = WzeroQuantity ? price : price * Wquantity;
                DateTime Wtime = DateTime.Now;
                newTransaction.date_time = Wtime;
                float Wfee = 0;
                newTransaction.fee = Wfee;
                newTransaction.note = "";

                watchTransactionData = newTransaction;
                //
                // APPEND DATA TO FIELDS
                APPEND_WATCH_FIELD(newTransaction);
                break;
        }
    }

    public void OpenTransaction(string id, TransactionType type)
    {
        // open transaction gui
        OPEN_TRANSACTION_OPTION(id, type);
    }

    private void OPEN_TRANSACTION_OPTION(string id, TransactionType type)
    {
        switch (type)
        {
            case TransactionType.BUY:
                BUY_OPTION();
                SetTransactionValues(id, type);
                break;
            case TransactionType.SELL:
                SELL_OPTION();
                SetTransactionValues(id, type);
                break;
            case TransactionType.WATCH:
                WATCH_OPTION();
                SetTransactionValues(id, type);
                break;
            default:
                BUY_OPTION();
                SetTransactionValues(id, type);
                break;
        }
    }


    public void CLOSE_ADD_TRANSACTION()
    {
        ADD_SAVE_TRANSACTION_BANNER.SetActive(false);
    }

    /// <summary>
    /// BUTTON SCRIPT - ATTACH TO BUTTON SAVE BUY TRANSACTION 
    /// </summary>
    public void SAVE_BUY_TRANSACTION_TO_PORTFOLIO()
    {
        // GET NEWLY ENTERED TRANSACTION VALUES
        Transaction new_transaction = GET_NEW_TRANSACTION(TransactionType.BUY);
        // SAVE TRANSACTION TO PORTFOLIO[0]
        transaction_number++;
        SaveManager.instance.state.portfolioList[0].transactions[transaction_number] = new_transaction;
        SaveManager.instance.Save();
        Debug.Log("BUY TRANSACTION COMPLETED.. SAVED COIN WITH PRICE: " + SaveManager.instance.state.portfolioList[0].transactions[0].price);
    }

    /// <summary>
    /// BUTTON SCRIPT - ATTACH TO BUTTON SAVE SELL TRANSACTION 
    /// </summary>
    public void SAVE_SELL_TRANSACTION_TO_PORTFOLIO()
    {
        // GET NEWLY ENTERED TRANSACTION VALUES
        Transaction new_transaction = GET_NEW_TRANSACTION(TransactionType.SELL);

        // SAVE TRANSACTION TO PORTFOLIO[0]
        transaction_number++;
        SaveManager.instance.state.portfolioList[0].transactions[transaction_number] = new_transaction;
        SaveManager.instance.Save();

        Debug.Log("BUY TRANSACTION COMPLETED.. SAVED COIN WITH PRICE: " + SaveManager.instance.state.portfolioList[0].transactions[transaction_number].price);
    }

    /// <summary>
    /// BUTTON SCRIPT - ATTACH TO BUTTON SAVE WATCH TRANSACTION 
    /// </summary>
    public void SAVE_WATCH_TRANSACTION_TO_PORTFOLIO()
    {
        // GET NEWLY ENTERED TRANSACTION VALUES
        Transaction new_transaction = GET_NEW_TRANSACTION(TransactionType.WATCH);

        // SAVE TRANSACTION TO PORTFOLIO[0]
        transaction_number++;
        SaveManager.instance.state.portfolioList[0].transactions[transaction_number] = new_transaction;
        SaveManager.instance.Save();

        Debug.Log("WATCH TRANSACTION COMPLETED.. SAVED COIN WITH PRICE: " + SaveManager.instance.state.portfolioList[0].transactions[transaction_number].price);
    }


    private Transaction GET_NEW_TRANSACTION(TransactionType type)
    {
        Transaction new_transaction = new Transaction();

        switch (type)
        {
            case TransactionType.BUY:
                new_transaction = GetBuyTransaction();
                break;
            case TransactionType.SELL:
                new_transaction = GetSellTransaction();
                break;
            case TransactionType.WATCH:
                new_transaction = GetWatchTransaction();
                break;
            default:
                new_transaction = GetBuyTransaction();
                break;
        }

        return new_transaction;
    }

    private Transaction GetBuyTransaction()
    {
        Transaction buy_transaction = new Transaction();

        buy_transaction.type = TransactionType.BUY;

        buy_transaction.exchange = Btext_Exchange.text;
        buy_transaction.trading_pair = Btext_TradingPair.text;

        float price = float.Parse(Btext_PriceUSD.text);
        buy_transaction.price = price;

        float quantity = float.Parse(Btext_Quantity.text);
        buy_transaction.quantity = quantity;

        bool BzeroQuantity = quantity == 0;
        buy_transaction.total_cost = BzeroQuantity ? price : price * quantity;

        DateTime Btime = DateTime.Parse(Btext_DateTime.text);
        buy_transaction.date_time = Btime;

        float Bfee = float.Parse(Btext_Fee.text);
        buy_transaction.fee = Bfee;

        buy_transaction.note = Btext_Note.text;

        return buy_transaction;
    }

    private Transaction GetSellTransaction()
    {
        Transaction sell_transaction = new Transaction();

        sell_transaction.type = TransactionType.SELL;

        sell_transaction.exchange = Stext_Exchange.text;
        sell_transaction.trading_pair = Stext_TradingPair.text;

        float price = float.Parse(Stext_PriceUSD.text);
        sell_transaction.price = price;

        float quantity = float.Parse(Stext_Quantity.text);
        sell_transaction.quantity = quantity;

        bool SzeroQuantity = quantity == 0;
        sell_transaction.total_cost = SzeroQuantity ? price : price * quantity;

        DateTime Stime = DateTime.Parse(Stext_DateTime.text);
        sell_transaction.date_time = Stime;

        float Sfee = float.Parse(Stext_Fee.text);
        sell_transaction.fee = Sfee;

        sell_transaction.note = Stext_Note.text;

        return sell_transaction;
    }

    private Transaction GetWatchTransaction()
    {
        Transaction watch_transaction = new Transaction();

        watch_transaction.type = TransactionType.WATCH;

        watch_transaction.exchange = "Global Average";
        string trading_pair = watchTransactionData.trading_pair;
        watch_transaction.trading_pair = trading_pair;

        float price = watchTransactionData.price;
        watch_transaction.price = price;

        float quantity = float.Parse(Wtext_Quantity.text);
        watch_transaction.quantity = quantity;

        bool WzeroQuantity = quantity == 0;
        watch_transaction.total_cost = WzeroQuantity ? price : price * quantity;

        DateTime Wtime = DateTime.Now;
        watch_transaction.date_time = Wtime;

        float Wfee = 0;
        watch_transaction.fee = Wfee;

        watch_transaction.note = "";

        return watch_transaction;
    }

}
