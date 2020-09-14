using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Favourites : MonoBehaviour
{

    #region Singleton
    private static Favourites instance;
    public static Favourites Instance
    {
        get
        {
            if (instance == null) { instance = FindObjectOfType(typeof(Favourites)) as Favourites; }
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
        star_image = GetComponent<Image>();
    }

    public bool isFavourite;
    public string ID;
    private Image star_image;

    public void  ADD_TO_FAVOURITES()
    {
        ID = transform.parent.transform.parent.name;

        isFavourite = false;

        foreach (string crypto in AppHandler.Instance.FAVOURITE_CRYPTOS)
        {
            if (crypto.Equals(ID))
            {
                isFavourite = true;
                break;
            }
            else { continue; }
        }

        if (isFavourite)
        {
            // remove from list
            AppHandler.Instance.FAVOURITE_CRYPTOS.Remove(ID);
            SaveListToArray();
            // change sprite img
            star_image.sprite = AppHandler.Instance.emptyStar;
        }
        else
        {
            // add to list
            AppHandler.Instance.FAVOURITE_CRYPTOS.Add(ID);
            SaveListToArray();
            // change sprite img
            star_image.sprite = AppHandler.Instance.filledStar;
        }
    }

    private void SaveListToArray()
    {
        SaveManager.instance.state.favouriteList = AppHandler.Instance.FAVOURITE_CRYPTOS.ToArray();
        SaveManager.instance.Save();
    }


}
