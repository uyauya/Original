using UnityEngine;
using System.Collections;

// ★★★マップ座標を扱うクラス★★★
public class MapAxis{
	public struct Axis_XZ{ public int x,z; }	// int型の構造体を宣言
	
	private Axis_XZ		beforeAxis;		// 移動前の位置座標
	private Axis_XZ		nowAxis;		// 現在(移動後)の位置座標
	private Axis_XZ		differenceAxis;	// 差分『現在位置－移動前位置』の格納要
	
	//private GameObject	player;			// プレイヤーオブジェクトへの参照用変数
	private MapSize	size;			// マップサイズへの参照用変数
	private Vector3		scale;			// 床ブロックのサイズ用
    public BattleManager battleManager;

	// ■■■床ブロックのサイズを返す■■■
	public Vector3 getScale(){ return scale; }
		
	// ■■■コンストラクタ■■■
	public MapAxis(BattleManager battleManager, MapSize size , Vector3 scale){
		this.beforeAxis.x = 0;
		this.beforeAxis.x = 0;
		this.nowAxis.x = 0;
		this.nowAxis.z = 0;
		this.differenceAxis.x = 0;
		this.differenceAxis.z = 0;
        this.battleManager = battleManager;
		//this.player	= player;		// 引数で受け渡された変数を参照する様に設定。
		this.size	= size;			// 引数で受け渡された変数を参照する様に設定。
		this.scale	= scale;		// Vector3型は値そのものがコピーされる。
	}
	
	// ■■■プレイヤーの初期座標を設定 (マップ中央座標)■■■
	public void initialize(){
		nowAxis.x = size.getHx();		// 初期位置(移動後の座標)は、半マップサイズ
		nowAxis.z = size.getHz();       // 初期位置(移動後の座標)は、半マップサイズ
        battleManager.Player.transform.position = new Vector3(nowAxis.x*scale.x , battleManager.Player.transform.position.y , nowAxis.z*scale.z);	// プレイヤーの位置を移動
	}
	
	// ■■■プレイヤーの現在座標を取得■■■
	private void setNowAxisX(){
		nowAxis.x = Mathf.FloorToInt((battleManager.Player.transform.position.x + scale.x/2) / scale.x);	// (現在位置+ブロック幅/2) / ブロック幅
	}
	private void setNowAxisZ(){
		nowAxis.z = Mathf.FloorToInt((battleManager.Player.transform.position.z + scale.z/2) / scale.z);	// (現在位置+ブロック幅/2) / ブロック幅
	}
	private void setNowAxis(){
		nowAxis.x = Mathf.FloorToInt((battleManager.Player.transform.position.x + scale.x/2) / scale.x);	// (現在位置+ブロック幅/2) / ブロック幅
		nowAxis.z = Mathf.FloorToInt((battleManager.Player.transform.position.z + scale.z/2) / scale.z);	// (現在位置+ブロック幅/2) / ブロック幅
	}
	
	// ■■■『現在位置－移動前位置』の更新■■■
	private void setDifferenceAxis(){
		differenceAxis.x = nowAxis.x - beforeAxis.x;	// Zの座標差分を取得
		differenceAxis.z = nowAxis.z - beforeAxis.z;	// Zの座標差分を取得
	}
	
	// ■■■『現在位置－移動前位置』の更新　(正面方向の更新のみ)■■■
	private void setDifferenceAxis_Z_FrontOnly(){
		differenceAxis.x = 0;							// Xの座標差分は常に０
		differenceAxis.z = nowAxis.z - beforeAxis.z;	// Zの座標差分を取得
		
		if(differenceAxis.z < 0){						// Zの座標差分がマイナスなら
			nowAxis.z = beforeAxis.z;					// 現在座標Zは前回と同じままにする
			differenceAxis.z = 0;						// Zの座標差分なし.
		}
	}
	
	// ■■■プレイヤー位置座標をの更新■■■
	public void updateAxis(){
		beforeAxis	= nowAxis;			// 移動前の位置座標を取得
		setNowAxisZ();					// 現在の位置座標Zを取得
		setDifferenceAxis_Z_FrontOnly();	// 『現在位置－移動前位置』の更新　(正面方向の更新のみ)
	}
	
	// ■■■差分『現在位置－移動前位置』を返す■■■
	public Axis_XZ getDefferenceAxis(){ return differenceAxis; }
	
	// ■■■マップ端の位置座標を返す（現在位置±半マップサイズ）■■■
	public int getAxis_MapStartX(){ return nowAxis.x - size.getHx(); }
	public int getAxis_MapStartZ(){ return nowAxis.z - size.getHz(); }
	public int getAxis_MapEndX()  { return nowAxis.x + size.getHx(); }
	public int getAxis_MapEndZ()  { return nowAxis.z + size.getHz(); }

	// ■■■プレイヤーの現在座標を返す■■■
	public Axis_XZ getNowAxis(){ return nowAxis; }
}