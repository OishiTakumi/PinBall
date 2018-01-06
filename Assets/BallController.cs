using UnityEngine;
using System.Collections;
using UnityEngine.UI;//UIをスクリプトから扱えるように

public class BallController : MonoBehaviour
{
    //ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -6.5f;

    //ゲームオーバを表示するテキスト
    private GameObject gameoverText;
  
    public Text ScoreLabel;//スコア―を表示するテキストUI格納

    public Text ScoreText; // スコアを表示する

    public Text HighScoreText;// ハイスコアを表示する

    // スコア
    private int Score;

    // ハイスコア
    private int HighScore;

    // PlayerPrefsで保存するためのキー
    private string HighScoreKey = "HighScore";
   

    void Start() // Use this for initialization
    {
        Initialize();
        //シーン中のGameOverTextオブジェクトを取得
        this.gameoverText = GameObject.Find("GameOverText");
    }

    // Update is called once per frame
    void Update()
    {
        //ScoreLabel.text = "スコア―:" + Score;//スコア―を表示 
        
        // スコアがハイスコアより大きければ
        if (HighScore < Score)
        {
            HighScore = Score;
        }

        // スコア・ハイスコアを表示する
        ScoreText.text = Score.ToString();
        HighScoreText.text = HighScore.ToString();
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "SmallCloudTag")
        {
            UpdateScore(10);
        }
        else if (other.gameObject.tag == "LargeCloudTag")
        {
            UpdateScore(20);  //大きい雲と衝突した場合の処理
        }
        else if (other.gameObject.tag == "LargeStarTag")
        {
            UpdateScore(30);  //大きい星と衝突した場合の処理
        }

        //ボールが画面外に出た場合
        if (this.transform.position.z < this.visiblePosZ)
        {
            //GameoverTextにゲームオーバを表示
            this.gameoverText.GetComponent<Text>().text = "Game Over";
        }
    }


    // ゲーム開始前の状態に戻す
    private void Initialize()
    {
        // スコアを0に戻す
        Score = 0;

        // ハイスコアを取得する。保存されてなければ0を取得する。
        HighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    // ポイントの追加
    public void AddPoint(int point)
    {
        Score = Score + point;
    }

    void UpdateScore(int score)
    {
        Score += score;//小さい雲と衝突した場合の処理

        // スコアがハイスコアより大きければ
        if (HighScore < Score)
        {
            HighScore = Score;
        }
        // スコア・ハイスコアを表示する
        ScoreText.text = Score.ToString();
        HighScoreText.text = HighScore.ToString();
    }

    // ハイスコアの保存
    public void Save()
    {
        // ハイスコアを保存する
        PlayerPrefs.SetInt(HighScoreKey, HighScore);
        PlayerPrefs.Save();

        // ゲーム開始前の状態に戻す
        Initialize();
    }
}

