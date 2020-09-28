
[System.Serializable]
public class Portfolio {
    public int id;
    public string name;
    public Transaction[] transactions;

    public Portfolio() { 
    }

    public Portfolio(int id, string name, Transaction[] transactions)
    {
        this.id = id;
        this.name = name;
        this.transactions = transactions;
    }
}
