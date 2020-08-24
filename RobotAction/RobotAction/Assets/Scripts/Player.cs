using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 個々の車軸の情報
    [SerializeField]
    private List<AxleInfo> axleInfos;
    //ホイールに適用可能な最大トルク
    [SerializeField]
    private float maxMotorTorque = 400;
    // 適用可能な最大ハンドル角度
    [SerializeField]
    private float maxSteeringAngle = 10;

    public void FixedUpdate()
    {
        var motor = maxMotorTorque * Input.GetAxis("Vertical");
        var steering = -maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (var axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }
}

// 車軸の情報を表します。
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    //このホイールがエンジンにアタッチされているかどうか
    public bool motor;
    // このホイールがハンドルの角度を反映しているかどうか
    public bool steering;
}