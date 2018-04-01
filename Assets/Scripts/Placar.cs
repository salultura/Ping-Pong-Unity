using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placar : MonoBehaviour {

    [SerializeField]
    private Text txt_scorePlayer1;
    [SerializeField]
    private Text txt_scorePlayer2;

    private float scorePlayer1 = 0;
    private float scorePlayer2 = 0;

    void Start () {
        txt_scorePlayer1.text = scorePlayer1.ToString();
        txt_scorePlayer2.text = scorePlayer2.ToString();
    }
	
    public void PontoPlayer1()
    {
        scorePlayer1 += 1;
        txt_scorePlayer1.text = scorePlayer1.ToString();        
    }

    public void PontoPlayer2()
    {
        scorePlayer2 += 1;
        txt_scorePlayer2.text = scorePlayer2.ToString();
    }
}
