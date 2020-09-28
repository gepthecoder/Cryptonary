using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Transaction
{
    public TransactionType type;

    public string exchange;
    public string trading_pair;

    public float price;
    public float quantity;
    public float total_cost;
    
    public DateTime date_time;

    public float fee;

    public string note;

    public Transaction(){
    }

    public Transaction(TransactionType type, string exchange, string trading_pair,
        float price, float quatity, float total_cost, DateTime time, float fee, string note)
    {
        this.type = type;
        this.exchange = exchange;
        this.trading_pair = trading_pair;
        this.price = price;
        this.quantity = quatity;
        this.total_cost = total_cost;
        this.date_time = time;
        this.fee = fee;
    }
}

public enum TransactionType { BUY, SELL, WATCH, NONE, }
