using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour {
    //HingiJointコンポーネントを入れる
    private HingeJoint myHingeJoint;

    //初期の傾き
    private float defaultAngle = 20;
    //弾いた時の傾き
    private float flickAngle = -20;

    // Use this for initialization
    void Start () {
       
        //HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
    }

    // Update is called once per frame
    void Update()
    {
        //左矢印キーを押した時左フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //右矢印キーを押した時右フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
        }

        //矢印キー離された時フリッパーを元に戻す
        if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
        }


        //全てのタッチの情報をfor文で取り出す
        for (int i = 0; i < Input.touches.Length; i++)
        {

            //配列の中のi番目のタッチ情報を取り出している
            Touch t = Input.GetTouch(i);

            //タッチ位置がを取り出す
            Vector2 touchPos = t.position;

            //画面の半分の横幅をscreenHalfWidth変数に設定
            float screenHalfWidth = Screen.width / 2; ;

            if (t.phase == TouchPhase.Began)
            {
                //タッチしたとき
                if (screenHalfWidth < touchPos.x && tag == "RightFripperTag")//タッチをした瞬間に行う処理,右のフリッパーを動かす
                {
                    {
                        SetAngle(this.flickAngle);
                    }
                }

                if (screenHalfWidth > touchPos.x && tag == "LeftFripperTag")//タッチをした瞬間に行う処理,左のフリッパーを動かす
                {
                    {
                        SetAngle(this.flickAngle);
                    }

                }
            }

            else if (t.phase == TouchPhase.Ended)
            {
                if (screenHalfWidth < touchPos.x && tag == "RightFripperTag")　//タッチを外した瞬間に行う処理,右のフリッパーを元に戻す
                {
                    {
                        SetAngle(this.defaultAngle);
                    }
                }

                if (screenHalfWidth > touchPos.x && tag == "LeftFripperTag") //タッチを外した瞬間に行う処理,左のフリッパーを元に戻す
                {
                    {
                        SetAngle(this.defaultAngle);
                    }

                }
            }
        }
    }
       
    //フリッパーの傾きを設定
    public void SetAngle(float angle)
    {
          JointSpring jointSpr = this.myHingeJoint.spring;
          jointSpr.targetPosition = angle;
          this.myHingeJoint.spring = jointSpr;
    }
    
}
