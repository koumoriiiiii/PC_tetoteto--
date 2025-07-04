using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;


    //ミノの状態
    bool Is_mino_move = false;
    bool Is_mino_hold;

    //prediction中のミノ番号
    public float prediction_mino_number = 4;
    bool Is_mino_prediction = true;

    //自身が何ミノか判断
    string My_is_mino;
    //ミノ左右移動のクールタイム
    float spwan_left;
    float spawn_right;
    public float previousTime;
    // minoの落ちる早さ
    float fallTime;
    //落下速度番号
    //リセットしたときの基準時間リセット
    float new_fallTime;
public float dwonSpeed = 3f;

    public float keydownspanTime = 0;

    // ステージの大きさ
    private static int width = 10;
    private static int height = 20;

    // mino回転
    public Vector3 rotationPoint;

    //ハードドロップする前の座標
    public Vector3 Mino_posi;

    //ミノがどれだけ回転してるか
    float mino_rotate = 0;

    // grid
    private static Transform[,] grid = new Transform[width, height];


    void Start(){
        //retryの連結
        retry retry;
        GameObject obj = GameObject.Find("Button");
        retry = obj.GetComponent<retry>();
        //gamemanagementの連結
        GameManagement gameManagement;
        GameObject ok = GameObject.Find("GameManagement");
        gameManagement = ok.GetComponent<GameManagement>();

        //基準時間取得
        new_fallTime = retry.new_falltime;

           //Componentを取得
        audioSource = GetComponent<AudioSource>();

        //自身のオブジェクト名を取得
        My_is_mino = this.gameObject.name;

        //最初に生成される４個のミノの番号割り当て
        if(gameManagement.first_create_mino == 1){
            prediction_mino_number = 1;
            gameManagement.first_create_mino += 1;
            FindObjectOfType<SpawnMino>().NewMino();
        }else if(gameManagement.first_create_mino == 2){
            Debug.Log("p");
            prediction_mino_number = 2;
            gameManagement.first_create_mino += 1;
            FindObjectOfType<SpawnMino>().NewMino();
        }else if(gameManagement.first_create_mino == 3){
            prediction_mino_number = 3;
            gameManagement.first_create_mino += 1;
            FindObjectOfType<SpawnMino>().NewMino();
        }else if(gameManagement.first_create_mino == 4){
            prediction_mino_number = 4;
            gameManagement.first_create_mino += 1;
        }


    }

    void Update()
    {
        GameManagement gameManagement;
        GameObject ok = GameObject.Find("GameManagement");
        gameManagement = ok.GetComponent<GameManagement>();
        if(gameManagement.IsPause){
            return;
        }
        if(Is_mino_prediction){
        PredictionMino();
        }
        else if(Is_mino_move){
        MinoMovememt();
        }
        else if(Is_mino_hold){
        MinoHold();
        }
    }

//ホールドされてるミノの動き
    private void MinoHold(){
        GameManagement gameManagement;
        GameObject ok = GameObject.Find("GameManagement");
        gameManagement = ok.GetComponent<GameManagement>();
        //ホールドされてるミノをもどす
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            if(gameManagement.hold_mino_Ok){
            transform.position = new Vector3(4,17,-1);
            Is_mino_hold = false;
            Is_mino_move = true;
            Gost_move();
            return;
                }
            }
    }

//prediction中のミノの動き
    private void PredictionMino(){
        GameManagement gameManagement;
        GameObject ok = GameObject.Find("GameManagement");
        gameManagement = ok.GetComponent<GameManagement>();

        if(gameManagement.Is_mino_prediction_change){
            prediction_mino_number -= 1;
            gameManagement.how_many_predictionNumber += 1;
            if(gameManagement.how_many_predictionNumber == 3){
                gameManagement.Is_mino_prediction_change = false;
                gameManagement.how_many_predictionNumber = 0;
            }
        }

        //prediction_numberが1になると動かせるように状態変更
        if(prediction_mino_number == 1){
            transform.position = new Vector3(4,17,-1);
            Is_mino_prediction = false;
            Is_mino_move = true;
            Gost_move();
        }
        if(prediction_mino_number == 2){
            transform.position = new Vector3(13,12,-1);
        }
        if(prediction_mino_number == 3){
            transform.position = new Vector3(13,7,-1);
        }
        if(prediction_mino_number == 4){
            transform.position = new Vector3(13,2,-1);
        }
    }

    private void MinoMovememt()
    {
        //ゲームマネージャーとの連結
        GameManagement gameManagement;
        GameObject ok = GameObject.Find("GameManagement");
        gameManagement = ok.GetComponent<GameManagement>();

        //落下速度変化
        FallTime();

        if(keydownspanTime > 0)
        {
            keydownspanTime -= 1;
        }

        //ホールド！！
        if(gameManagement.hold_mino_Ok){
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){

        Is_mino_move = false;
        Is_mino_hold = true;
            //回転の初期化
            transform.rotation = Quaternion.identity;
            mino_rotate = 0;
            this.gameObject.transform.position = new Vector3(-4,17,0);
            gameManagement.hold_mino_Ok = false;
            if(gameManagement.first_Hold){
                FindObjectOfType<SpawnMino>().NewMino();
                gameManagement.Is_mino_prediction_change = true;
                gameManagement.first_Hold = false;
            }
            return;
        }
        }

        // 左矢印キーで左に動く
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            //長押しして次移動する時間（最初）
            spwan_left = 10;
            
            if (!ValidMovement()) 
            {
                transform.position -= new Vector3(-1, 0, 0);
            }else {
                //音(sound1)を鳴らす
                audioSource.PlayOneShot(sound2);
                Gost_move();
            }
            }else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
            spwan_left -= 1;
            if(spwan_left == 0){
            transform.position += new Vector3(-1, 0, 0);
                if (!ValidMovement()) 
            {
                transform.position -= new Vector3(-1, 0, 0);
            }else{
                //音(sound1)を鳴らす
                audioSource.PlayOneShot(sound2);
                Gost_move();
            }
            }
            if(spwan_left == 0){
            spwan_left = 5;
            }
            
        }

        // 右矢印キーで左に動く
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            //長押しして次移動する時間（最初）
            spawn_right = 10;
            
            if (!ValidMovement()) 
            {
                transform.position -= new Vector3(1, 0, 0);
            }else{
                //音(sound1)を鳴らす
                audioSource.PlayOneShot(sound2);
                Gost_move();
            }
            }else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
            spawn_right -= 1;
            if(spawn_right == 0){
            transform.position += new Vector3(1, 0, 0);
                if (!ValidMovement()) 
            {
                transform.position -= new Vector3(1, 0, 0);
            }else{
                //音(sound1)を鳴らす
                audioSource.PlayOneShot(sound2);
                Gost_move();
            }
            }
            if(spawn_right == 0){
            spawn_right = 5;
            }
            
        }
        //下矢印キーで移動する
        if (Input.GetKey(KeyCode.DownArrow) && keydownspanTime == 0
        || Input.GetKey(KeyCode.S) && keydownspanTime == 0) 
        {
            transform.position += new Vector3(0, -1, 0);

            keydownspanTime = 7;
            
            if (!ValidMovement()) 
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckLines();
                this.enabled = false;
                FindObjectOfType<SpawnMino>().NewMino();
                gameManagement.Is_mino_prediction_change = true;
                //ミノがホールドできるようにする
                gameManagement.hold_mino_Ok = true;
            }


        }//自動で下げる
        else if ( Time.time - previousTime >= fallTime) 
        {
            transform.position += new Vector3(0, -1, 0);
            
            if (!ValidMovement()) 
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckLines();
                this.enabled = false;
                FindObjectOfType<SpawnMino>().NewMino();
                gameManagement.Is_mino_prediction_change = true;
                //ミノがホールドできるようにする
                gameManagement.hold_mino_Ok = true;
            }
            previousTime = Time.time;
        }

                //ハードドロップ
        if(Input.GetMouseButtonDown(1)){
            while(ValidMovement()){
                transform.position -= new Vector3(0, 1, 0);
            }
               //音(sound1)を鳴らす
                audioSource.PlayOneShot(sound1);
            transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckLines();
                this.enabled = false;
                FindObjectOfType<SpawnMino>().NewMino();
                gameManagement.Is_mino_prediction_change = true;
                //ミノがホールドできるようにする
                gameManagement.hold_mino_Ok = true;
        }

        if (Input.GetMouseButtonDown(0) && Time.time > 0.1 && My_is_mino != "Omino(Clone)"|| Input.GetKeyDown(KeyCode.Space) && Time.time > 0.1 && My_is_mino != "Omino(Clone)")
        {
            // ブロックの回転
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            //回転して重なったら戻す
            if(!ValidMovement()){
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, -1), 90);
            }else{
                mino_rotate += 1;
                if(mino_rotate == 4){
                    mino_rotate = 0;
                }
                Gost_move();
            }
        }
    }



    // 今回の追加 ラインがあるか？確認
    public void CheckLines()
    {
        //gamemanagementの連結
        GameManagement gameManagement;
        GameObject ok = GameObject.Find("GameManagement");
        gameManagement = ok.GetComponent<GameManagement>();
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                FindObjectOfType<GameManagement>().Gost_trash();
                gameManagement.part_posi = this.transform.position;
                gameManagement.IsPause = true;
            }
        }
    }

    // 今回の追加 列がそろっているか確認
    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }
        FindObjectOfType<GameManagement>().AddScore();
        return true;
    }

    // 今回の追加 ラインを消す
    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }

    }

    // 今回の追加 列を下げる
    public void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void AddToGrid() 
    {
        
        foreach (Transform children in transform) 
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundX, roundY] = children;
        }
        
    }

    // minoの移動範囲の制御
    bool ValidMovement()
    {

        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            // minoがステージよりはみ出さないように制御
            if (roundX < 0 || roundX >= width || roundY < 0 || roundY >= height)
            {
                return false;
            }
            if (grid[roundX, roundY] != null)
            {
                return false;
            }
            

        }
        return true;
    }

        //落下速度変化
    void FallTime(){
        if(Time.time - new_fallTime <= 10){
            fallTime = 1f;
            FindObjectOfType<GameManagement>().Speed_mark_change1();
        }else if(Time.time - new_fallTime <= 30 && Time.time - new_fallTime > 10){
            fallTime = 0.9f;
            FindObjectOfType<GameManagement>().Speed_mark_change2();
        }else if(Time.time - new_fallTime <= 60 && Time.time - new_fallTime > 30){
            fallTime = 0.8f;
            FindObjectOfType<GameManagement>().Speed_mark_change3();
        }else if(Time.time - new_fallTime <= 90 && Time.time - new_fallTime > 60){
            fallTime = 0.6f;
            FindObjectOfType<GameManagement>().Speed_mark_change4();
        }else if(Time.time - new_fallTime <= 120 && Time.time - new_fallTime > 90){
            fallTime = 0.4f;
            FindObjectOfType<GameManagement>().Speed_mark_change5();
        }else if(Time.time - new_fallTime <= 150 && Time.time - new_fallTime > 120){
            fallTime = 0.2f;
            FindObjectOfType<GameManagement>().Speed_mark_change6();
        }else if(Time.time - new_fallTime <= 170 && Time.time - new_fallTime > 150){
            fallTime = 0.15f;
            FindObjectOfType<GameManagement>().Speed_mark_change7();
        }else if(Time.time - new_fallTime <= 190 && Time.time - new_fallTime > 170){
            fallTime = 0.12f;
            FindObjectOfType<GameManagement>().Speed_mark_change8();
        }else if(Time.time - new_fallTime <= 210 && Time.time - new_fallTime > 190){
            fallTime = 0.1f;
            FindObjectOfType<GameManagement>().Speed_mark_change9();
        }else if(Time.time - new_fallTime <= 230 && Time.time - new_fallTime > 210){
            fallTime = 0.08f;
            FindObjectOfType<GameManagement>().Speed_mark_change10();
        }else if(Time.time - new_fallTime > 230 ){
            fallTime = 0.04f;
            FindObjectOfType<GameManagement>().Speed_mark_change_Max();
        }
    }

    //ゴーストくんの移動
    void Gost_move(){
        //gamemanagementの連結
        GameManagement gameManagement;
        GameObject ok = GameObject.Find("GameManagement");
        gameManagement = ok.GetComponent<GameManagement>();

            //いったん座標記録
            Mino_posi = this.transform.position;
            //いったんハードドロップ
            while(ValidMovement()){
                transform.position -= new Vector3(0, 1, 0);
            }
            transform.position -= new Vector3(0, -1, 0);
            //座標をゲームマネージャーに送る
            gameManagement.Gost_posi = this.transform.position;
            //もとの座標にもどす
            transform.position = Mino_posi;
            gameManagement.Gost_rotate = mino_rotate;

            if(My_is_mino == "Imino(Clone)"){
                FindObjectOfType<GameManagement>().Gost_move_I();
            }
            if(My_is_mino == "Jmino(Clone)"){
                FindObjectOfType<GameManagement>().Gost_move_J();
            }
            if(My_is_mino == "Lmino(Clone)"){
                FindObjectOfType<GameManagement>().Gost_move_L();
            }
            if(My_is_mino == "Omino(Clone)"){
                FindObjectOfType<GameManagement>().Gost_move_O();
            }
            if(My_is_mino == "Smino(Clone)"){
                FindObjectOfType<GameManagement>().Gost_move_S();
            }
            if(My_is_mino == "Tmino(Clone)"){
                FindObjectOfType<GameManagement>().Gost_move_T();
            }
            if(My_is_mino == "Zmino(Clone)"){
                FindObjectOfType<GameManagement>().Gost_move_Z();
            }
    }
}

