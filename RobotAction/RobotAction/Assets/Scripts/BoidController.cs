using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    // ドローンの総数
    public int Num;

    public Vector3 AveragePos;     // ポジションの平均値
    public float AverageAngle;     // ローテーションの平均値

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
        // 子オブジェクトの総数
        Num = this.transform.childCount;
        // 合計(position)
        Vector3 totalPos = Vector3.zero;
        // 合計(rotation)
        float totalAngle = 0;

        for (int i = 0; i < Num; i++)
        {
            // 子オブジェクトのの参照
            GameObject child = transform.GetChild(i).gameObject;

            // Position
            // 子オブジェクトのポジション取得
            Vector3 childPos = child.transform.position;
            totalPos += childPos;

            // Rotation
            // 子オブジェクトのローテーション取得
            float childRot = child.transform.localEulerAngles.y;
            totalAngle += childRot;
        }
        // 平均(position)
        AveragePos = totalPos / Num;
        // 平均(rotation)
        AverageAngle = totalAngle / Num;
    }
}
