using UnityEngine;
using System.Collections;

// ★★★マップ配列を扱うクラス★★★
public class MapArray{
	protected string			name;				// この配列の名前(オブジェクト名として使用)
	protected GameObject		folder;				// 作成したマップオブジェクトを入れるフォルダー
	protected GameObject[,]		arr;				// マップオブジェクト格納用の配列
	protected MapSize		size;				// マップサイズへの参照用変数
	protected MapAxis		axis;				// マップ座標への参照用変数	
	protected int				SIGN;				// 符合用。
	protected MapAxis.Axis_XZ arrAxis_Start;	// マップ始点の配列座標格納用
	protected MapAxis.Axis_XZ arrAxis_End;		// マップ終点の配列座標格納用 
	
	// ■■■コンストラクタ■■■
	public MapArray (string name, MapSize size, MapAxis axis){
		this.name	= name;											// このクラスの名前を設定
		this.folder = new GameObject (this.name + "_Folder");		// フォルダーオブジェクトを作成 (コンストラクタとして、オブジェクト名を渡す)
		this.arr	= new GameObject[size.getX (), size.getZ ()];	// マップサイズ分の配列を宣言
		this.size	= size;											// 引数で受け渡された変数を参照する様に設定。
		this.axis	= axis;											// 引数で受け渡された変数を参照する様に設定。
		
		this.SIGN	= 1;
	}

	// ■■■マップ始点／終点の配列座標の取得■■■
	protected void renewal_arrAxis(){
		arrAxis_Start.x	= getArrayNum_X(axis.getAxis_MapStartX());	// マップ始点のX位置座標を、配列座標Xとして取得
		arrAxis_Start.z	= getArrayNum_Z(axis.getAxis_MapStartZ());	// マップ始点のZ位置座標を、配列座標Zとして取得
		arrAxis_End.x	= getArrayNum_X(axis.getAxis_MapEndX());	// マップ終点のX位置座標を、配列座標Xとして取得
		arrAxis_End.z	= getArrayNum_Z(axis.getAxis_MapEndZ());	// マップ終点のZ位置座標を、配列座標Zとして取得
	}
	
	// ■■■位置座標Xを配列座標に変換して返す■■■
	protected int getArrayNum_X (int value){
		int ret = value % size.getX ();
		if (ret < 0) { ret += size.getX();	}
		return ret;
	}
	
	// ■■■位置座標Zを配列座標に変換して返す■■■
	protected int getArrayNum_Z (int value){
		int ret = value % size.getZ();
		if (ret < 0) { ret += size.getZ();	}
		return ret;
	}
	
	// ■■■指定された位置座標に指定オブジェクトを作成、配列に格納■■■
	protected void cleateObject(GameObject prefab , MapAxis.Axis_XZ arg){ cleateObject(prefab , arg.x , arg.z); }
	protected void cleateObject(GameObject prefab , int x , int z){
		int arr_x = getArrayNum_X(x);		// 配列座標Xを取得
		int arr_z = getArrayNum_Z(z);		// 配列座標Zを取得
		
		if (arr [arr_x, arr_z] != null) { 
			MonoBehaviour.Destroy (arr[arr_x, arr_z].gameObject); 	// 配列内にオブジェクトが格納されていたら、削除する
			arr [arr_x, arr_z] = null;
		}
		Vector3 scale			= prefab.transform.localScale;										// 受け渡されたオブジェクトのサイズを格納 (以下の式を見易くする為)
		Vector3 pos				= new Vector3 (scale.x*x , SIGN*scale.y/2 , scale.z*z);				// 位置の算出
		GameObject obj			= GameObject.Instantiate (prefab, pos, Quaternion.identity) as GameObject;		// プレハブ作成
		obj.name				= name + "[" + arr_x + "," + arr_z + "]";		// 作成したオブジェクトの名前変更
		obj.transform.parent	= folder.transform;						// 作成したオブジェクトの親を、フォルダーにする
		arr[arr_x, arr_z]		= obj;									// 作成したオブジェクトを、配列に格納
	}

	// ■■■指定された位置座標のオブジェクトを削除する■■■
	protected void deleteObject(MapAxis.Axis_XZ arg){ deleteObject(arg.x , arg.z); }
	protected void deleteObject(int x , int z){
		int arr_x = getArrayNum_X (x);		// 配列座標Xを取得
		int arr_z = getArrayNum_Z (z);		// 配列座標Zを取得
		if (arr [arr_x, arr_z] != null) {	// 配列内にオブジェクトが格納されていたら、削除する
			MonoBehaviour.Destroy (arr [arr_x, arr_z].gameObject);
			arr [arr_x, arr_z] = null;
		}
	}
}

// ★★★マップ配列(床)を扱うクラス★★★
public class MapArrayBlock : MapArray{
	private GameObject[] block;		// 床ブロックの参照用
	
	// ■■■コンストラクタ■■■
	public MapArrayBlock (GameObject[] obj , string name , MapSize size , MapAxis axis) : base(name , size , axis){
		this.block = obj;		// 引数で受け渡された変数を参照する様に設定。
		this.SIGN = -1;			// 床側なので、符合をマイナスに。
	}
	
	// ■■■指定座標の、床タイプ番号を返す (座標x+zをプレファブ数で割った余値を返す)■■■
	private int getInt_BlockType(MapAxis.Axis_XZ arg){ return getInt_BlockType(arg.x, arg.z); }
	private int getInt_BlockType(int x, int z){
		if (block.Length != 0) {
			int ret = (x + z)% block.Length ;
			if(ret < 0){ ret += block.Length; }
			return ret;
		} else {
			return 0;		// 配列の中身が無い場合は、０を返す
		}
	}
	
	// ■■■スタート時のマップ(床)作成■■■
	public void startMap_Create(){
		renewal_arrAxis();			// スタート時点のマップ始点／終点の配列座標を取得
		if(block.Length != 0){
			int n;	// 床タイプ番号
			for (int z=0 ; z< size.getZ() ; z++) {
				for (int x=0 ; x< size.getX() ; x++) {
					n = getInt_BlockType(x, z);				// 床タイプ番号を取得
					cleateObject(block [n], x, z);			// オブジェクト作成
				}
			}
		}else{
			return;		// 配列の中身が無い場合は、何もせず抜ける
		}
	}
	
	// ■■■マップ(床)の更新■■■
	public void renewal(){
		renewal_Z();	// 行方向のマップ(床)更新
	}
	
	// ▼▼▼列方向のマップ(床)更新▼▼▼
	private void renewal_X(){
		if(axis.getDefferenceAxis().x != 0){		// 位置座標の差分Zが０で無いなら
			MapAxis.Axis_XZ posAxis;			// 位置座標の始点
			posAxis.x = (axis.getDefferenceAxis().x > 0) ? axis.getAxis_MapEndX() : axis.getAxis_MapStartX(); // 始点Xはマップ端　（現在位置±半マップサイズ）
			posAxis.z = axis.getAxis_MapStartZ();	// 始点Zはマップ端　（現在位置－半マップサイズ）
			
			if(block.Length != 0){
				int n;	// 床タイプ番号
				for (int z=0 ; z< size.getZ() ; z++) {
					n = getInt_BlockType(posAxis.x , z+posAxis.z);			// 床タイプ番号を取得
					cleateObject(block[n] , posAxis.x , z+posAxis.z);		// オブジェクト作成
				}
			}else{
				return;		// 配列の中身が無い場合は、何もせず抜ける
			}
		}
	}
	
	// ▼▼▼行方向のマップ(床)更新▼▼▼
	private void renewal_Z(){
		if(axis.getDefferenceAxis().z != 0){		// 位置座標の差分Zが０で無いなら
			MapAxis.Axis_XZ posAxis;			// 位置座標の始点
			posAxis.x = axis.getAxis_MapStartX();	// 始点Xはマップ端　（現在位置－半マップサイズ）
			posAxis.z = (axis.getDefferenceAxis().z > 0) ? axis.getAxis_MapEndZ() : axis.getAxis_MapStartZ(); // 始点Zはマップ端　（現在位置±半マップサイズ）
			
			if(block.Length != 0){
				int n;	// 床タイプ番号
				for (int x=0 ; x< size.getX() ; x++) {
					n = getInt_BlockType(x+posAxis.x , posAxis.z);			// 床タイプ番号を取得
					cleateObject(block[n] , x+posAxis.x , posAxis.z);		// オブジェクト作成
				}
			}else{
				return;		// 配列の中身が無い場合は、何もせず抜ける
			}
		}
	}
}

// ★★★マップ配列(地上)を扱うクラス★★★
public partial class MapArrayFloor : MapArray{
	private GameObject[] wall;		// 壁オブジェクトの参照用
	private GameObject[] obstacle;	// 障害物オブジェクトの格納用
	private bool[,]			isMoveArea;	// プレイヤーが移動出来るエリアを格納する配列
	private bool			isMapJage;	// マップが適性の場合 ture

	// ■■■コンストラクタ■■■
	public MapArrayFloor (string name, MapSize size, MapAxis axis) : base(name , size , axis){
		en_folder = new GameObject("EN");		// 敵格納用のフォルダを、ENという名前で作成
		isMapJage = false;		// 初期マップ適性は、とりあえずfalse
		isMoveArea = new bool[size.getX() , size.getZ()];	// 配列確保。および、全てfalseに初期化
		for(int z=0 ; z< size.getZ() ; z++){
			for(int x=0 ; x< size.getX() ; x++){
				isMoveArea[x,z] = false;
			}
		}
	}
	
	// ■■■壁オブジェクトのセット■■■
	public void setWall(GameObject[] obj){ wall = obj; }

	// ■■■障害物オブジェクトのセット■■■
	public void setObstacle(GameObject[] obj){ obstacle = obj; }
	
	// ■■■スタート時のマップ(地上)作成■■■
	public void startMap_Create(){
		renewal_arrAxis();			// スタート時点のマップ始点／終点の配列座標を取得
		for (int z=0 ; z< size.getZ() ; z++) {
			cleateObject(wall[0], axis.getAxis_MapStartX(), z);					// 0列目に壁オブジェクト作成
			cleateObject(wall[0], axis.getAxis_MapEndX(), z);		// (マップサイズ－１)列目に壁オブジェクト作成
			checkAxis_setIsMoveArea(axis.getNowAxis());		// 該当座標を基準に調べ、プレイヤーが移動出来るエリアをtrueに変えていく
			check_isMapJage();			// マップ上端一列を確認し、プレイヤーが移動出来るかどうかを確認する
		}
	}

	// ■■■マップ上端一列を確認し、プレイヤーが移動出来るかどうかを確認する■■■
	private void check_isMapJage(){
		for(int x=1 ; x< size.getX()-1 ; x++){
			if(getIsMoveArea(axis.getAxis_MapStartX() + x , axis.getAxis_MapEndZ())){	// 指定座標(マップ上端一列)を調査し、プレイヤーが移動出来るエリアを見つけたら
				isMapJage = true;			// そのマップは移動可能
				return;						// 以後は調査する必要がないため、処理を抜ける.
			}
		}
		isMapJage = false;			// そのマップは移動不可
	}
	
	// ■■■指定された位置座標のisMoveAreaの値を返す■■■
	private bool getIsMoveArea(int x , int z){
		return isMoveArea[getArrayNum_X(x) , getArrayNum_Z(z)];
	}

	// ■■■受け渡された位置座標を配列座標に変換し、プレイヤーが移動出来るエリアをtrueに変えていく■■■
	private void checkAxis_setIsMoveArea(MapAxis.Axis_XZ arg){ checkAxis_setIsMoveArea(arg.x , arg.z); }
	private void checkAxis_setIsMoveArea(int x , int z){
		checkAxisArray_setIsMoveArea(getArrayNum_X(x) , getArrayNum_Z(z));	// 配列座標を受け渡し、プレイヤーが移動出来るエリアをtrueに変えていく
	}
	
	// ■■■受け渡された配列座標から、プレイヤーが移動出来るエリアをtrueに変えていく■■■
	private void checkAxisArray_setIsMoveArea(int arr_x , int arr_z){
		if(arr[arr_x , arr_z] == null && !isMoveArea[arr_x , arr_z]){	// その配列座標が空(オブジェクトが入っていない)　かつ　falseなら
			isMoveArea[arr_x,arr_z] = true;		// その配列座標を true にする
			checkDir_IsMoveArea(arr_x , arr_z);	// その配列座標の周囲(上下左右)を調べ、オブジェクトが配置されていなければtrueに変える
		}
	}
	
	// ■■■受け渡された配列座標の周囲(上下左右)を調べ、オブジェクトが配置されていなければtrueに変える■■
	private void checkDir_IsMoveArea(int arr_x , int arr_z){
		if (arr_x != arrAxis_Start.x) {	// マップ左端の座標でないなら
			checkAxisArray_setIsMoveArea (getArrayNum_X (arr_x - 1), arr_z);	// x-1座標を、プレイヤーが移動出来るエリアかどうか調べる
		}
		if (arr_z != arrAxis_Start.z) {	// マップ下端の座標でないなら
			checkAxisArray_setIsMoveArea (arr_x, getArrayNum_Z (arr_z - 1));	// z-1座標を、プレイヤーが移動出来るエリアかどうか調べる
		}
		if (arr_x != arrAxis_End.x) {		// マップ左端の座標でないなら
			checkAxisArray_setIsMoveArea (getArrayNum_X (arr_x + 1), arr_z);	// x+1座標を、プレイヤーが移動出来るエリアかどうか調べる
		}
		if (arr_z != arrAxis_End.z) {		// マップ下端の座標でないなら
			checkAxisArray_setIsMoveArea (arr_x, getArrayNum_Z (arr_z + 1));	// z+1座標を、プレイヤーが移動出来るエリアかどうか調べる
		}
	}
	// ■■■マップ(地上)の更新■■■
	public void renewal(){
		renewal_wallZ();		// 行方向のマップ(壁)の更新
		randomSet_obstacleZ();	// 行方向のマップ(障害物)の更新
		
		if(axis.getDefferenceAxis().z > 0){		// 位置座標の差分Zがプラスなら
			renewal_arrAxis();			// 配列座標を更新
			renewal_IsMoveArea();		// マップ上端一列の、プレイヤー移動可能判定を更新
			check_isMapJage();			// マップ適性を再確認する。
			
			while(!isMapJage){			// マップ適性がfalseの間、ループ。
				correctMap();			// マップ修正
				check_isMapJage();		// マップ適性を再確認する。
			}
		}
	}
	
	// ▼▼▼行方向のマップ(壁)更新▼▼▼
	private void renewal_wallZ(){
		if(axis.getDefferenceAxis().z != 0){		// 位置座標の差分Zが０で無いなら
			if(wall.Length != 0){
				int z = (axis.getDefferenceAxis().z > 0) ? axis.getAxis_MapEndZ() : axis.getAxis_MapStartZ(); // Zはマップ端　（現在位置±半マップサイズ）
				cleateObject(wall[0] , axis.getAxis_MapStartX() , z);		// オブジェクト作成
				cleateObject(wall[0] , axis.getAxis_MapEndX() , z);		// オブジェクト作成
			}else{
				return;		// 配列の中身が無い場合は、何もせず抜ける
			}
		}
	}

	// ▼▼▼行方向のマップ(障害物)更新▼▼▼
	private void randomSet_obstacleZ(){
		if(axis.getDefferenceAxis().z > 0){		// 位置座標の差分Zがプラスなら
			MapAxis.Axis_XZ posAxis;			// 位置座標の始点
			posAxis.x = axis.getAxis_MapStartX();	// 始点Xはマップ始端　（現在位置－半マップサイズ）
			posAxis.z = axis.getAxis_MapEndZ();		// Zはマップ終端　（現在位置＋半マップサイズ）
			
			if(obstacle.Length != 0){
				for (int x=1 ; x< size.getX()-1 ; x++) {
					if(Random.Range(0,100) < 30){		// 0～99のランダム値で、30未満だったら
						cleateObject(obstacle[0] , x+posAxis.x , posAxis.z);		// オブジェクト作成
					}else{
						deleteObject(x+posAxis.x , posAxis.z);	// オブジェクトの作成
					}
				}
			}
		}
	}

	// ■■■指定された位置座標のisMoveAreaをfalseにする■■■
	private void setIsMoveArea_False(int x , int z){
		isMoveArea[getArrayNum_X(x) , getArrayNum_Z(z)] = false;	// 指定座標をfalseに
	}
	
	// ■■■マップ上端一列のisMoveAreaを更新■■■
	private void renewal_IsMoveArea(){
		for(int x=1 ; x< size.getX()-1 ; x++){
			setIsMoveArea_False(axis.getAxis_MapStartX() + x , axis.getAxis_MapEndZ());	// 指定座標をfalseにする
		}
		
		for(int x=1 ; x< size.getX()-1 ; x++){
			checkRenewalMapAxis_IsMoveArea(axis.getAxis_MapStartX() + x , axis.getAxis_MapEndZ());	// 指定座標とその真下座標を調べ、プレイヤーが通れるなら、プレイヤーが移動出来るエリアをtrueに変えていく
		}
	}
	
	// ■■■受け渡された位置座標と、その下の座標を調べ、プレイヤーが通れるなら、プレイヤーが移動出来るエリアをtrueに変えていく■■■
	private void checkRenewalMapAxis_IsMoveArea(int x , int z){
		int arr_x = getArrayNum_X(x);		// 配列座標Xを取得
		int arr_z = getArrayNum_Z(z);		// 配列座標Zを取得
		
		if(arr[arr_x , arr_z] != null || isMoveArea[arr_x , arr_z]){ return; }	// 該当座標が空でない、もしくはtrueなら、処理を抜ける
		
		if(isMoveArea[arr_x , getArrayNum_Z(arr_z-1)]){		// 真下の座標がtrueなら
			checkAxisArray_setIsMoveArea(arr_x , arr_z);	// 
		}
	}
	
	// ■■■マップを修正■■■
	private void correctMap(){
		int arr_x = getArrayNum_X(axis.getAxis_MapStartX() + Random.Range(1 , size.getX()-1));	// ランダムで列を設定
		int arr_z = arrAxis_End.z;
		
		if(arr[arr_x , arr_z] != null){	// 指定座標にオブジェクトが入っていた場合
			if(isMoveArea[arr_x , getArrayNum_Z(arr_z-1)]){	// 指定座標Z-1がプレイヤー通行可能エリアだった場合
				deleteObject(arr_x , arr_z);		// その座標のオブジェクトを破壊
				checkAxisArray_setIsMoveArea(arr_x , arrAxis_End.z);			// プレイヤーが移動出来るエリアをtrueに変えていく
			}
		}
	}
}
