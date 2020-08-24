using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    public int num;        // ドローンの総数

    public Vector3 AveragePos;     // ポジションの平均値
    public float AverageRot;     // ローテーションの平均値

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 平均値
        Center();
    }

    /// <summary>
    /// ポジション、ローテーションの平均
    /// </summary>
    void Center()
    {
        // 子オブジェクトの総数を代入
        num = this.transform.childCount;
        // 合計(position)
        Vector3 totalpos = Vector3.zero;
        // 合計(rotation)
        float totalrot = 0;

        for (int i = 0; i < num; i++)
        {
            // 子オブジェクトのの参照
            GameObject Childs = transform.GetChild(i).gameObject;

            // Position
            // 子オブジェクトのポジション取得
            Vector3 childpos = Childs.transform.position;
            totalpos += childpos;

            // Rotation
            // 子オブジェクトのローテーション取得
            float childrot = Childs.transform.localEulerAngles.y;
            totalrot += childrot;
        }
        // 平均(position)
        AveragePos = totalpos / num;
        // 平均(rotation)
        AverageRot = totalrot / num;
    }
}
