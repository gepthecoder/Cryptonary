/*
Cryptocurrencies.cs
contains an array of Cryptocurrencies with a few methods to get the icon(s) and current price.
*/



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Web;
using SimpleJSON;

namespace Cryptocurrency
{

    public class Cryptocurrencies
    {
        /// <summary>
        /// Array of the identifier name symbol.
        /// </summary>
        private static string[,] IdNameSymbol = new string[100, 3]
        {
        {"bitcoin","Bitcoin","BTC"},
        {"ethereum","Ethereum","ETH"},
        {"ripple","Ripple","XRP"},
        {"bitcoin-cash","Bitcoin Cash","BCH"},
        {"litecoin","Litecoin","LTC"},
        {"cardano","Cardano","ADA"},
        {"neo","NEO","NEO"},
        {"stellar","Stellar","XLM"},
        {"eos","EOS","EOS"},
        {"iota","IOTA","MIOTA"},
        {"dash","Dash","DASH"},
        {"monero","Monero","XMR"},
        {"nem","NEM","XEM"},
        {"ethereum-classic","Ethereum Classic","ETC"},
        {"vechain","VeChain","VEN"},
        {"tron","TRON","TRX"},
        {"tether","Tether","USDT"},
        {"lisk","Lisk","LSK"},
        {"bitcoin-gold","Bitcoin Gold","BTG"},
        {"qtum","Qtum","QTUM"},
        {"omisego","OmiseGO","OMG"},
        {"nano","Nano","NANO"},
        {"icon","ICON","ICX"},
        {"zcash","Zcash","ZEC"},
        {"binance-coin","Binance Coin","BNB"},
        {"verge","Verge","XVG"},
        {"steem","Steem","STEEM"},
        {"digixdao","DigixDAO","DGD"},
        {"bytecoin-bcn","Bytecoin","BCN"},
        {"populous","Populous","PPT"},
        {"stratis","Stratis","STRAT"},
        {"dogecoin","Dogecoin","DOGE"},
        {"siacoin","Siacoin","SC"},
        {"rchain","RChain","RHOC"},
        {"waves","Waves","WAVES"},
        {"maker","Maker","MKR"},
        {"status","Status","SNT"},
        {"bitshares","BitShares","BTS"},
        {"waltonchain","Waltonchain","WTC"},
        {"electroneum","Electroneum","ETN"},
        {"veritaseum","Veritaseum","VERI"},
        {"0x","0x","ZRX"},
        {"aeternity","Aeternity","AE"},
        {"decred","Decred","DCR"},
        {"augur","Augur","REP"},
        {"ardor","Ardor","ARDR"},
        {"zclassic","ZClassic","ZCL"},
        {"revain","Revain","R"},
        {"kucoin-shares","KuCoin Shares","KCS"},
        {"komodo","Komodo","KMD"},
        {"hshare","Hshare","HSR"},
        {"ark","Ark","ARK"},
        {"gas","Gas","GAS"},
        {"basic-attention-token","Basic Attention Token","BAT"},
        {"loopring","Loopring","LRC"},
        {"digibyte","DigiByte","DGB"},
        {"dragonchain","Dragonchain","DRGN"},
        {"zilliqa","Zilliqa","ZIL"},
        {"bytom","Bytom","BTM"},
        {"monacoin","MonaCoin","MONA"},
        {"syscoin","Syscoin","SYS"},
        {"dent","Dent","DENT"},
        {"aion","Aion","AION"},
        {"reddcoin","ReddCoin","RDD"},
        {"polymath-network","Polymath","POLY"},
        {"aelf","aelf","ELF"},
        {"pivx","PIVX","PIVX"},
        {"qash","QASH","QASH"},
        {"golem-network-tokens","Golem","GNT"},
        {"byteball","Byteball Bytes","GBYTE"},
        {"nebulas-token","Nebulas","NAS"},
        {"dentacoin","Dentacoin","DCN"},
        {"factom","Factom","FCT"},
        {"kyber-network","Kyber Network","KNC"},
        {"bitcore","Bitcore","BTX"},
        {"ethos","Ethos","ETHOS"},
        {"gxshares","GXShares","GXS"},
        {"chainlink","ChainLink","LINK"},
        {"iostoken","IOStoken","IOST"},
        {"funfair","FunFair","FUN"},
        {"cryptonex","Cryptonex","CNX"},
        {"salt","SALT","SALT"},
        {"power-ledger","Power Ledger","POWR"},
        {"zcoin","ZCoin","XZC"},
        {"kin","Kin","KIN"},
        {"nxt","Nxt","NXT"},
        {"pillar","Pillar","PLR"},
        {"request-network","Request Network","REQ"},
        {"cindicator","Cindicator","CND"},
        {"particl","Particl","PART"},
        {"bancor","Bancor","BNT"},
        {"enigma-project","Enigma","ENG"},
        {"neblio","Neblio","NEBL"},
        {"smartcash","SmartCash","SMART"},
        {"singularitynet","SingularityNET","AGI"},
        {"wax","WAX","WAX"},
        {"vertcoin","Vertcoin","VTC"},
        {"tenx","TenX","PAY"},
        {"maidsafecoin","MaidSafeCoin","MAID"},
        {"ignis","Ignis","IGNIS"}
        };


        /// <summary>
        /// Gets the Array of identifier name symbols.
        /// </summary>
        /// <returns>The identifier name symbols.</returns>
        public static string[,] getIdNameSymbols()
        {
            return IdNameSymbol;
        }

        /// <summary>
        /// Icon types.
        /// </summary>
        public enum iconTypes
        {
            white,
            black,
            color
        }


        /// <summary>
        /// Gets the icon. using the symbol
        /// </summary>
        /// <returns>The icon.</returns>
        /// <param name="symbol">Symbol.</param>
        /// <param name="iconType">Icon type.</param>
        public static Sprite getIcon(string symbol, iconTypes iconType)
        {
            return Resources.Load("cryptocurrency-icons-master/128/" + iconType.ToString() + "/" + symbol.ToLower(), typeof(Sprite)) as Sprite;
        }

        /*EDIT THIS LINE*/
        private static string API_KEY = "057688f8-ecd8-4761-8c8f-2c872ef4ef22";

        /// <summary>
        /// Gets the price. using the id
        /// </summary>
        /// <returns>The price.</returns>
        /// <param name="id">Identifier.</param>
        public static float getPrice(string symbol)
        {
            //https://coinmarketcap.com/api/
            //var www = new WWW("https://api.coinmarketcap.com/v1/ticker/" + id + "/?limit=0");
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = symbol;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            

            JSONNode data = JSON.Parse(client.DownloadString(URL.ToString()));
            float price = data["data"][symbol]["quote"]["USD"]["price"];
            if (float.IsNaN(price))
            {
                Debug.LogError("Unable to Find volume, returning -1.0f");
                price = -1.0f;
            }
            return price;

        }

        /// <summary>
        /// Gets the volume. using the id
        /// </summary>
        /// <returns>The price.</returns>
        /// <param name="id">Identifier.</param>
        public static float[] getPercentChangedAll(string symbol)
        {
            //https://coinmarketcap.com/api/
            //var www = new WWW("https://api.coinmarketcap.com/v1/ticker/" + id + "/?limit=0");
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = symbol;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");

            float[] percents = new float[3];

            JSONNode data = JSON.Parse(client.DownloadString(URL.ToString()));
            float pc1h = data["data"][symbol]["quote"]["USD"]["percent_change_1h"];
            float pc24h = data["data"][symbol]["quote"]["USD"]["percent_change_24h"];
            float pc7d = data["data"][symbol]["quote"]["USD"]["percent_change_7d"];

            percents[0] = pc1h;
            percents[1] = pc24h;
            percents[2] = pc7d;

            return percents;

        }

        /// <summary>
        /// Gets the volume. using the id
        /// </summary>
        /// <returns>The price.</returns>
        /// <param name="id">Identifier.</param>
        public static float getVolume(string symbol)
        {
            //https://coinmarketcap.com/api/
            //var www = new WWW("https://api.coinmarketcap.com/v1/ticker/" + id + "/?limit=0");
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = symbol;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");


            JSONNode data = JSON.Parse(client.DownloadString(URL.ToString()));
            float volume = data["data"][symbol]["quote"]["USD"]["volume_24h"];

            if (float.IsNaN(volume))
            {
                Debug.LogError("Unable to Find Price, returning -1.0f");
                volume = -1.0f;
            }
            return volume;

        }



        /// <summary>
        /// Gets the market cap. using the id
        /// </summary>
        /// <returns>The total cap.</returns>
        /// <param name="id">Identifier.</param>
        public static float getMarketCap(string symbol)
        {
            //https://coinmarketcap.com/api/
            //var www = new WWW("https://api.coinmarketcap.com/v1/ticker/" + id + "/?limit=0");
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = symbol;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");


            JSONNode data = JSON.Parse(client.DownloadString(URL.ToString()));
            float cap = data["data"][symbol]["quote"]["USD"]["market_cap"];

            if (float.IsNaN(cap))
            {
                Debug.LogError("Unable to Find Price, returning -1.0f");
                cap = -1.0f;
            }
            return cap;

        }

        /// <summary>
        /// Gets the rank of crypto. using the id
        /// </summary>
        /// <returns>cmc rank.</returns>
        /// <param name="id">Identifier.</param>
        public static int getRank(string symbol)
        {
            //https://coinmarketcap.com/api/
            //var www = new WWW("https://api.coinmarketcap.com/v1/ticker/" + id + "/?limit=0");
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = symbol;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");


            JSONNode data = JSON.Parse(client.DownloadString(URL.ToString()));

            if (data.IsNull) { Debug.Log("data is not available!!");
                return 0;
            }
            int rank = data["data"][symbol]["cmc_rank"];
            return rank;
        }
    }
    

    public class CryptocurrencyRecord
    {
        #region Variables
        private string _id;
        public string id
        {
            get
            {
                return _id;
            }
        }

        private string _name;
        public string name
        {
            get
            {
                return _name;
            }
        }

        private string _symbol;
        public string symbol
        {
            get
            {
                return _symbol;
            }
        }



        private float _price_usd;
        public float price_usd
        {
            get
            {
                return _price_usd;
            }
        }

        private float _volume_24h;
        public float volume_24h
        {
            get
            {
                return _volume_24h;
            }
        }


        private float _percent_change_1h;
        public float percent_change_1h
        {
            get
            {
                return _percent_change_1h;
            }
        }

        private float _percent_change_24h;
        public float percent_change_24h
        {
            get
            {
                return _percent_change_24h;
            }
        }

        private float _percent_change_7d;
        public float percent_change_7d
        {
            get
            {
                return _percent_change_7d;
            }
        }

        private float _market_cap;
        public float market_cap
        {
            get
            {
                return _market_cap;
            }
        }

        private float _last_updated;
        public float last_updated
        {
            get
            {
                return _last_updated;
            }
        }

        private Sprite _icon;
        public Sprite icon
        {
            get
            {
                return _icon;
            }
        }
        #endregion

        /*EDIT THIS LINE*/
        private static string API_KEY = "057688f8-ecd8-4761-8c8f-2c872ef4ef22";

        public CryptocurrencyRecord(string inputSymbol, Cryptocurrencies.iconTypes iconType)
        {
            string[,] IdNameSymbol = Cryptocurrencies.getIdNameSymbols();

            for (int i = 0; i < IdNameSymbol.Length / 3; i++)
            {
                if (inputSymbol == IdNameSymbol[i, 2])
                {
                    _id = IdNameSymbol[i, 0];
                    _name = IdNameSymbol[i, 1];
                    _symbol = IdNameSymbol[i, 2];
                    break;
                }
            }

            _icon = Resources.Load("cryptocurrency-icons-master/128/" + iconType.ToString() + "/" + symbol.ToLower(), typeof(Sprite)) as Sprite;

            var Data = getData(symbol)["data"][symbol]["quote"]["USD"];

            _price_usd = Data["price"];
            _volume_24h = Data["volume_24h"];
            _percent_change_1h = Data["percent_change_1h"];
            _percent_change_24h = Data["percent_change_24h"];
            _percent_change_7d = Data["percent_change_7d"];
            _market_cap = Data["market_cap"];
            _last_updated = Data["last_updated"];

        }


        //public Dictionary<string, object> getData(string symbol)
        public JSONNode getData(string symbol)
        {

            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = symbol;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");


            JSONNode data = JSON.Parse(client.DownloadString(URL.ToString()));
            //return data["data"][symbol]["quote"]["USD"];
            return data;

        }

    }



}




