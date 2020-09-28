using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Search : MonoBehaviour
{
    #region Singleton
    private static Search instance;
    public static Search Instance
    {
        get
        {
            if (instance == null) { instance = FindObjectOfType(typeof(Search)) as Search; }
            return instance;
        }
        set { instance = value; }
    }
    #endregion

    void Awake()
    {
        instance = this;
    }

    public string ID;

    public void ADD_TRANSACTION()
    {
        ID = transform.name;
        Debug.Log("Added transaction: " + ID);
        TransactionManager.Instance.OpenTransaction(ID, TransactionType.BUY);
    }
}
