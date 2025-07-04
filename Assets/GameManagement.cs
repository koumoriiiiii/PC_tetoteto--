using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    //効果音
    public AudioClip sound1;
    AudioSource audioSource;

        // mino回転
    public Vector3 rotationPoint;

    //時間停止するかどうか
    public bool IsPause;
    float PauseCount = 0;

    //ミノホールドしていいかどうか
    public bool hold_mino_Ok;

    //最初のホールドかどうか
    public bool first_Hold = true;

    //hold_markの取得
    [SerializeField] private Renderer hold_mark;

    //最初に生成されるミノの認識
    public float first_create_mino = 1;

    //predictionを変化させて良いかどうか
    public bool Is_mino_prediction_change = false;
    //ミノのprediction_numberが何回変わったか認識
    public float how_many_predictionNumber = 0;

    //パーティクルの取得
    [SerializeField] private ParticleSystem particle;

    public GameObject particles;

    //パーティクルの位置を変更するための変数
    public Vector3 part_posi;


        //Speed_mark
    [SerializeField] private Renderer speed1;
    [SerializeField] private Renderer speed2;
    [SerializeField] private Renderer speed3;
    [SerializeField] private Renderer speed4;
    [SerializeField] private Renderer speed5;
    [SerializeField] private Renderer speed6;
    [SerializeField] private Renderer speed7;
    [SerializeField] private Renderer speed8;
    [SerializeField] private Renderer speed9;
    [SerializeField] private Renderer speed10;
    [SerializeField] private Renderer speed_max;

    //ゴーストくんオブジェクトを取得
    public GameObject Mino_gostI;
    public GameObject Mino_gostJ;
    public GameObject Mino_gostL;
    public GameObject Mino_gostO;
    public GameObject Mino_gostS;
    public GameObject Mino_gostT;
    public GameObject Mino_gostZ;

    //ゴースト君のレンダーを取得
    [SerializeField] private Renderer Mino_gostRI;
    [SerializeField] private Renderer Mino_gostRI1;
    [SerializeField] private Renderer Mino_gostRI2;
    [SerializeField] private Renderer Mino_gostRI4;

    [SerializeField] private Renderer Mino_gostRJ;
    [SerializeField] private Renderer Mino_gostRJ1;
    [SerializeField] private Renderer Mino_gostRJ2;
    [SerializeField] private Renderer Mino_gostRJ4;

    [SerializeField] private Renderer Mino_gostRL;
    [SerializeField] private Renderer Mino_gostRL1;
    [SerializeField] private Renderer Mino_gostRL2;
    [SerializeField] private Renderer Mino_gostRL4;

    [SerializeField] private Renderer Mino_gostRO;
    [SerializeField] private Renderer Mino_gostRO1;
    [SerializeField] private Renderer Mino_gostRO2;
    [SerializeField] private Renderer Mino_gostRO4;

    [SerializeField] private Renderer Mino_gostRS;
    [SerializeField] private Renderer Mino_gostRS1;
    [SerializeField] private Renderer Mino_gostRS2;
    [SerializeField] private Renderer Mino_gostRS4;

    [SerializeField] private Renderer Mino_gostRT;
    [SerializeField] private Renderer Mino_gostRT1;
    [SerializeField] private Renderer Mino_gostRT2;
    [SerializeField] private Renderer Mino_gostRT4;

    [SerializeField] private Renderer Mino_gostRZ;
    [SerializeField] private Renderer Mino_gostRZ1;
    [SerializeField] private Renderer Mino_gostRZ2;
    [SerializeField] private Renderer Mino_gostRZ4;

    //ゴーストくんの座標を代入する場所
    public Vector3 Gost_posi;

    //ゴーストくんの回転を代入する場所
    public float Gost_rotate;



    // スコア関連
    public Text scoreText;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
           //Componentを取得
        audioSource = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;

        Is_mino_prediction_change = false;

        //最初にゴースト非表示
        Gost_trash();

        Initialize();



        //ホールドマーク非表示
        hold_mark.enabled = false;

        //ホールドok
        hold_mino_Ok = true;
        Speed_mark();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hold_mino_Ok){
            hold_mark.enabled = true;
        }else if(hold_mino_Ok){
            hold_mark.enabled = false;
        }
        //ポーズカウントが一定以上たまったらポーズ解除
        if(IsPause){
            PauseCount += 1;
            if(PauseCount == 20){
                IsPause = false;
                PauseCount = 0;
            }
        }
    }

    // ゲーム開始前の状態に戻す
    private void Initialize()
    {
        // スコアを0に戻す
        score = 0;

    }

    // スコアの追加
    public void AddScore()
    {
        particles.transform.position = part_posi;
        particle.Play();
        audioSource.PlayOneShot(sound1);
        score += 100;
        scoreText.text = "Score: " + score.ToString();

        Debug.Log("Add 100");

    }

    void Speed_mark(){
        speed1.enabled = true;
        speed2.enabled = false;
        speed3.enabled = false;
        speed4.enabled = false;
        speed5.enabled = false;
        speed6.enabled = false;
        speed7.enabled = false;
        speed8.enabled = false;
        speed9.enabled = false;
        speed10.enabled = false;
        speed_max.enabled = false;
    }

    public void Speed_mark_change1(){
        speed1.enabled = true;
    }

    public void Speed_mark_change2(){
        speed2.enabled = true;
        speed1.enabled = false;
    }

        public void Speed_mark_change3(){
        speed3.enabled = true;
        speed2.enabled = false;
    }

        public void Speed_mark_change4(){
        speed4.enabled = true;
        speed3.enabled = false;
    }

        public void Speed_mark_change5(){
        speed5.enabled = true;
        speed4.enabled = false;
    }

        public void Speed_mark_change6(){
        speed6.enabled = true;
        speed5.enabled = false;
    }

        public void Speed_mark_change7(){
        speed7.enabled = true;
        speed6.enabled = false;
    }

        public void Speed_mark_change8(){
        speed8.enabled = true;
        speed7.enabled = false;
    }

        public void Speed_mark_change9(){
        speed9.enabled = true;
        speed8.enabled = false;
    }

        public void Speed_mark_change10(){
        speed10.enabled = true;
        speed9.enabled = false;
    }

        public void Speed_mark_change_Max(){
        speed_max.enabled = true;
        speed10.enabled = false;
    }

    public void Gost_trash(){
        Mino_gostRI.enabled = false;
        Mino_gostRI1.enabled = false;
        Mino_gostRI2.enabled = false;
        Mino_gostRI4.enabled = false;

        Mino_gostRJ.enabled = false;
        Mino_gostRJ1.enabled = false;
        Mino_gostRJ2.enabled = false;
        Mino_gostRJ4.enabled = false;

        Mino_gostRL.enabled = false;
        Mino_gostRL1.enabled = false;
        Mino_gostRL2.enabled = false;
        Mino_gostRL4.enabled = false;

        Mino_gostRO.enabled = false;
        Mino_gostRO1.enabled = false;
        Mino_gostRO2.enabled = false;
        Mino_gostRO4.enabled = false;

        Mino_gostRS.enabled = false;
        Mino_gostRS1.enabled = false;
        Mino_gostRS2.enabled = false;
        Mino_gostRS4.enabled = false;

        Mino_gostRT.enabled = false;
        Mino_gostRT1.enabled = false;
        Mino_gostRT2.enabled = false;
        Mino_gostRT4.enabled = false;

        Mino_gostRZ.enabled = false;
        Mino_gostRZ1.enabled = false;
        Mino_gostRZ2.enabled = false;
        Mino_gostRZ4.enabled = false;
    }

    //対応するゴーストくんを移動させる
    public void Gost_move_I(){
        Gost_trash();
        Mino_gostI.transform.rotation = Quaternion.identity;
        if(Gost_rotate == 1){
            Mino_gostI.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }else if(Gost_rotate == 2){
            Mino_gostI.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 180);
        }else if(Gost_rotate == 3){
            Mino_gostI.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 270);
        }
        Mino_gostRI.enabled = true;
        Mino_gostRI1.enabled = true;
        Mino_gostRI2.enabled = true;
        Mino_gostRI4.enabled = true;

        Mino_gostI.transform.position = Gost_posi;
    }

    public void Gost_move_J(){
        Gost_trash();
        Mino_gostJ.transform.rotation = Quaternion.identity;
        if(Gost_rotate == 1){
            Mino_gostJ.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }else if(Gost_rotate == 2){
            Mino_gostJ.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 180);
        }else if(Gost_rotate == 3){
            Mino_gostJ.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 270);
        }
        Mino_gostRJ.enabled = true;
        Mino_gostRJ1.enabled = true;
        Mino_gostRJ2.enabled = true;
        Mino_gostRJ4.enabled = true;

        Mino_gostJ.transform.position = Gost_posi;
    }

        public void Gost_move_L(){
            Gost_trash();
        Mino_gostL.transform.rotation = Quaternion.identity;
        if(Gost_rotate == 1){
            Mino_gostL.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }else if(Gost_rotate == 2){
            Mino_gostL.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 180);
        }else if(Gost_rotate == 3){
            Mino_gostL.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 270);
        }
        Mino_gostRL.enabled = true;
        Mino_gostRL1.enabled = true;
        Mino_gostRL2.enabled = true;
        Mino_gostRL4.enabled = true;

        Mino_gostL.transform.position = Gost_posi;
    }

    public void Gost_move_O(){
        Gost_trash();
        Mino_gostO.transform.rotation = Quaternion.identity;
        if(Gost_rotate == 1){
            Mino_gostO.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }else if(Gost_rotate == 2){
            Mino_gostO.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 180);
        }else if(Gost_rotate == 3){
            Mino_gostO.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 270);
        }
        Mino_gostRO.enabled = true;
        Mino_gostRO1.enabled = true;
        Mino_gostRO2.enabled = true;
        Mino_gostRO4.enabled = true;

        Mino_gostO.transform.position = Gost_posi;
    }

    public void Gost_move_S(){
        Gost_trash();
        Mino_gostS.transform.rotation = Quaternion.identity;
        if(Gost_rotate == 1){
            Mino_gostS.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }else if(Gost_rotate == 2){
            Mino_gostS.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 180);
        }else if(Gost_rotate == 3){
            Mino_gostS.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 270);
        }
        Mino_gostRS.enabled = true;
        Mino_gostRS1.enabled = true;
        Mino_gostRS2.enabled = true;
        Mino_gostRS4.enabled = true;

        Mino_gostS.transform.position = Gost_posi;
    }

    public void Gost_move_T(){
        Gost_trash();
        Mino_gostT.transform.rotation = Quaternion.identity;
        if(Gost_rotate == 1){
            Mino_gostT.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }else if(Gost_rotate == 2){
            Mino_gostT.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 180);
        }else if(Gost_rotate == 3){
            Mino_gostT.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 270);
        }
        Mino_gostRT.enabled = true;
        Mino_gostRT1.enabled = true;
        Mino_gostRT2.enabled = true;
        Mino_gostRT4.enabled = true;

        Mino_gostT.transform.position = Gost_posi;
    }

    public void Gost_move_Z(){
        Gost_trash();
        Mino_gostZ.transform.rotation = Quaternion.identity;
        if(Gost_rotate == 1){
            Mino_gostZ.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }else if(Gost_rotate == 2){
            Mino_gostZ.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 180);
        }else if(Gost_rotate == 3){
            Mino_gostZ.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 270);
        }
        Mino_gostRZ.enabled = true;
        Mino_gostRZ1.enabled = true;
        Mino_gostRZ2.enabled = true;
        Mino_gostRZ4.enabled = true;

        Mino_gostZ.transform.position = Gost_posi;
    }
}

