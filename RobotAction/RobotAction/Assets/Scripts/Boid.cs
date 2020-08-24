using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    // MainController型オブジェクト
    BoidController boid;

    // 速度
    public float Velocity;
    [SerializeField]
    float NormalSpeed,HighSpeed,RowSpeed;

    // 円の初期変数
    float CircleX;
    float CircleY;
    float CircleZ;
    // 半径
    float Radius;

    // 三平方の計算用変数
    float DiffX;
    float DiffY;
    float DiffZ;
    float TwoPointsDiff;

    // 距離間隔
    [SerializeField]
    float Distance;

    private void Awake()
    {
        // MainControllerのコンポーネント取得
        boid = GameObject.Find("ParentBoid").GetComponent<BoidController>(); ;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 速度を通常速度で初期化
        Velocity = NormalSpeed;

        Radius = Distance;
    }

    // Update is called once per frame
    void Update()
    {
        // 前進
        gameObject.transform.Translate(0, 0, Time.deltaTime * Velocity);
        // 中心よりxまたはzが2.0f以上離れているとき
        if (gameObject.transform.position.x - boid.AveragePos.x >= Distance ||
            gameObject.transform.position.x - boid.AveragePos.x <= -Distance ||
            gameObject.transform.position.z - boid.AveragePos.z >= Distance ||
            gameObject.transform.position.z - boid.AveragePos.z <= -Distance)
        {
            // 整列(Alingment)
            Transform transform = this.transform;
            Vector3 localAngle = transform.localEulerAngles;
            // 全体の角度の平均に回転させる
            localAngle.y = boid.AverageAngle;
            transform.localEulerAngles = localAngle;

            // 速度調整
            // 中心点よりも後にいたら速度を上げる
            // 中心点よりも前にいたら速度を下げる
            VelocityAdjustment(boid.AveragePos.z, gameObject.transform.position.z);
            
            // 結合(Cohesion)
            Cohesion();
        }
        // 当たり判定
        HitChk();
    }

    /// <summary>
    /// 速度調整
    /// </summary>
    /// <param name="firstTarget"></param>
    /// <param name="secondaryTarget"></param>
    void VelocityAdjustment(float firstTarget, float secondaryTarget)
    {
        if (firstTarget > secondaryTarget)
        {
            Velocity = HighSpeed;  // 速度を上げる
        }
        else if (firstTarget < secondaryTarget)
        {
            Velocity = RowSpeed;  // 速度を下げる
        }
        else
        {
            Velocity = NormalSpeed;  // 通常の速度にする
        }
    }

    /// <summary>
    /// 結合(Cohesion)
    /// </summary>
    void Cohesion()
    {
        // 中心に向かって移動する
        transform.position = Vector3.MoveTowards(transform.position,boid.AveragePos,Time.deltaTime * Velocity / Distance);
    }
    /// <summary>
    /// 当たり判定
    /// </summary>
    void HitChk()
    {
        // 初期化
        CircleX = transform.position.x;
        CircleY = transform.position.y;
        CircleZ = transform.position.z;
        for (int i = 0; i < boid.Num; i++)
        {
            // 一つ目のオブジェクト参照
            GameObject Childs = boid.transform.GetChild(i).gameObject;
            // 同じオブジェクトどうしで計算しないように
            if (this.gameObject == Childs)
            {
                continue;
            }
            // 子オブジェクトのコンポーネント取得
            var child = Childs.GetComponent<Boid>();
            // 三平方の定理
            // 円の当たり判定
            DiffX = CircleX - child.CircleX;
            DiffY = CircleY - child.CircleY;
            DiffZ = CircleZ - child.CircleZ;
            TwoPointsDiff = (DiffX * DiffX) + (DiffY * DiffY) + (DiffZ * DiffZ);
            // 当たり判定
            // 当たっている時
            if (TwoPointsDiff <= Radius + child.Radius)
            {
                // Separation(引き離し)
                // 速度調整
                // 衝突したオブジェクトよりも前にいたら速度を上げる
                // 衝突したオブジェクトよりも後にいたら速度を下げる
                VelocityAdjustment(gameObject.transform.position.z, child.transform.position.z);
                break;
            }
            else
            {
                // 通常の速度にする
                Velocity = NormalSpeed;
            }
        }
    }
}