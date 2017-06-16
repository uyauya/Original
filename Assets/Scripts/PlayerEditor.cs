using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

public class PlayerEditor : MonoBehaviour 

	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			// target は処理コードのインスタンスだよ！ 処理コードの型でキャストして使ってね！
			Character chara = target as Character;

			/* -- カスタム表示 -- */

			// -- 体力 --
			EditorGUILayout.LabelField( "体力(現在/最大)" );
			EditorGUILayout.BeginHorizontal();
			chara.hp        = EditorGUILayout.IntField( chara.hp, GUILayout.Width(48) );
			chara.maxHp     = EditorGUILayout.IntField( chara.maxHp, GUILayout.Width(48) );
			EditorGUILayout.EndHorizontal();

			// -- 速度 --
			chara.speed     = EditorGUILayout.FloatField( "速度", chara.speed );

			// -- 名前 --
			chara.charaName = EditorGUILayout.TextField( "名前", chara.charaName );

			// -- 友達 --
			List<GameObject> list = chara.friends;
			int i, len = list.Count;

			// 折りたたみ表示
			if( folding = EditorGUILayout.Foldout( folding, "友達" ) )
			{
				// リスト表示
				for( i = 0; i < len; ++i )
				{
					list[i] = EditorGUILayout.ObjectField( list[i], typeof(GameObject), true ) as GameObject;
				}

				GameObject go = EditorGUILayout.ObjectField( "追加", null, typeof(GameObject), true ) as GameObject;
				if( go != null )
					list.Add( go );
			}
		}
	}

