using UnityEngine;
using System.Collections;

public partial class MapArrayFloor{
	public GameObject[] enemy;			// 敵を格納する変数
	private GameObject	en_folder;		// 敵を格納するフォルダー
	private int			max_enemy = 20;	// 敵の最大数.
	
	// ■■■敵オブジェクトのセット■■■
	public void setEnemy(GameObject[] obj){ enemy = obj; }
	
	// ■■■敵出現用関数■■■
	public void enemyArrival(){
		if(en_folder.transform.childCount >= max_enemy){ return; }	// フォルダー内に敵の数が最大数以上なら、以降は処理しない。
		
		MapAxis.Axis_XZ posAxis;				// 位置座標の始点用
		posAxis.x = axis.getAxis_MapStartX();	// 始点Xはマップ始端　（現在位置－半マップサイズ）
		posAxis.z = axis.getAxis_MapEndZ();		// Zはマップ終端　（現在位置＋半マップサイズ）
		
		if(enemy.Length != 0){
			for (int x=1 ; x< size.getX()-1 ; x++) {
				if(en_folder.transform.childCount >= max_enemy){ return; }	// フォルダー内に敵の数が最大数以上なら、以降は処理しない。
				if(Random.Range(0,100) <= 10){ 					// 10%の確率で
					createEnemy(x+posAxis.x , posAxis.z);		// 敵オブジェクトの作成
				}
			}
		}
	}
	
	// ■■■敵オブジェクトの作成■■■
	private void createEnemy(MapAxis.Axis_XZ arg){ createEnemy(arg.x , arg.z); }
	private void createEnemy(int x , int z){
		if(enemy.Length == 0){ return; }	// 敵が格納されていないなら、処理を抜ける
		int arr_x = getArrayNum_X(x);		// 配列座標Xを取得
		int arr_z = getArrayNum_Z(z);		// 配列座標Zを取得
		
		if (arr [arr_x, arr_z] == null) {	// 配列内が空だったなら
			Vector3 scale			= axis.getScale();										// マップのブロックサイズを取得
			Vector3 pos				= new Vector3 (scale.x*x , 0 , scale.z*z);				// 位置の算出
			GameObject obj			= GameObject.Instantiate (enemy[Random.Range(0 , enemy.Length)], pos, Quaternion.Euler(0,180,0)) as GameObject;		// プレハブ作成
			obj.transform.parent	= en_folder.transform;									// 作成したオブジェクトの親を、フォルダーにする
		}
	}
}
