using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter Instance;

    public TMP_Text coinText;
    public int currentCoins = 0;
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        coinText.text = "COINS : " + currentCoins.ToString();
    }

    public void IncreaseCoins(int v)
    {
        currentCoins += v;
        coinText.text = "COINS : " + currentCoins.ToString();
    }
}
