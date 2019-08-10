using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rotate = rotate1;
    }

    void addAngle(float angle){
        var rot = transform.eulerAngles;
        rot.z += angle;
        transform.eulerAngles = rot;
    }
    void setAngle(float angle){
        var rot = transform.eulerAngles;
        rot.z = angle;
        transform.eulerAngles = rot;
    }

    void rotate1(){
        // 砲身を6秒で1回転させる.
        addAngle(60.0f*Time.deltaTime);
    }

    void rotate2(){
        // 砲身の根本からターゲットまでの線分を求める.
        var dist = target.position - transform.position;
        // 線分の長さを1にする.
        dist.Normalize();
        // ターゲットの角度を求める.
        float rad = Mathf.Atan2(dist.y,dist.x);
        // ラジアン角をオイラー角に変換して象限を合わせる.
        float angle = rad * Mathf.Rad2Deg - 90.0f;
        setAngle( angle );
    }

    void rotate3(){
        // 砲身の根本からターゲットまでの線分を求める.
        var dist = target.position - transform.position;
        // 線分の長さを1にする.
        dist.Normalize();
        // ターゲットの角度を求める.
        float rad = Mathf.Atan2(dist.y,dist.x);
        // ラジアン角をオイラー角に変換して象限を合わせる.
        float angle = rad * Mathf.Rad2Deg - 90.0f;
        // 今の砲身の角度
        float curAngle = transform.eulerAngles.z;
        // 砲身の角度をターゲットの角度に近づける.
        addAngle( (angle - curAngle)*0.1f );
    }

    void rotate4(){
        // 砲身の根本からターゲットまでの線分を求める.
        var dist = target.position - transform.position;
        // 線分の長さを1にする.
        dist.Normalize();
        // ターゲットの角度を求める.
        float rad = Mathf.Atan2(dist.y,dist.x);
        // ラジアン角をオイラー角に変換して象限を合わせる.
        float angle = rad * Mathf.Rad2Deg - 90.0f;
        // 今の砲身の角度
        float curAngle = transform.eulerAngles.z;
        // 近い方向に補正する.
        float distAngle = angle-curAngle;
        if ( distAngle > 180.0f ) distAngle -= 360.0f;
        if ( distAngle < -180.0f ) distAngle += 360.0f;

        addAngle( (distAngle)*0.1f );
    }

    void rotate5(){
        var dist = target.position - transform.position;
        float angle = Vector2.Angle(new Vector2(0,1),dist);
        setAngle(angle);
    }

    Action rotate;

    // Update is called once per frame
    void Update()
    {
        //rotate1();
        //rotate2();
        //rotate3();
        //rotate4();
        //rotate5();
        //rotate6();
        rotate.Invoke();
    }

    string [] buttonName ={
        "Rotate1",
        "Rotate2",
        "Rotate3",
        "Rotate4",
        "Rotate5",
    };
    void OnGUI(){
        int id = 0;
        foreach ( var name in buttonName ){
            if (GUI.Button(new Rect(10, 10+40*id, 100, 40), name)){
                OnButton(id);
            }
            id++;
        }
    }

    public void OnButton(int id){
        switch(id){
            case 0: rotate = rotate1; break;
            case 1: rotate = rotate2; break;
            case 2: rotate = rotate3; break;
            case 3: rotate = rotate4; break;
            case 4: rotate = rotate5; break;
        }
    }


}
