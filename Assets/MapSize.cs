using UnityEngine;
using System.Collections;

// ★★★マップサイズを扱うクラス★★★
public class MapSize{
	private const int DEFAULT_SIZE = 11;			// デフォルト値
	private int x;		// X方向のマップサイズ
	private int z;		// Z方向のマップサイズ
	private int hx;		// X方向の半マップサイズ
	private int hz;		// Z方向の半マップサイズ
	
	// ■■■コンストラクタ■■■
	public MapSize(int x , int z){
		if(x <= 0){ x = DEFAULT_SIZE; }			// ０以下の値を受け渡された場合は、デフォルト値にする
		if(z <= 0){ z = DEFAULT_SIZE; }			// ０以下の値を受け渡された場合は、デフォルト値にする
		
		this.x	= (x % 2 == 1) ? x : x+1;		// X方向のマップサイズを奇数化
		this.z	= (z % 2 == 1) ? z : z+1;		// Z方向のマップサイズを奇数化
		this.hx	= Mathf.FloorToInt(this.x/2);	// X方向の半マップサイズ(切り捨て)
		this.hz	= Mathf.FloorToInt(this.z/2);	// Z方向の半マップサイズ(切り捨て)
	}
	
	// ■■■各変数を返すゲッター関数■■■
	public int getX() { return x;  }		// X方向のマップサイズを返す
	public int getZ() { return z;  }		// Z方向のマップサイズを返す
	public int getHx(){ return hx; }		// X方向の半マップサイズを返す
	public int getHz(){ return hz; }		// Z方向の半マップサイズを返す
}
