using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class SpriteSeparater
{
	// 名称 / サイズの指定箇所
	// 全ての画像で各要素の位置があらかた決まっているのであればX/Y座標も入れておく
	// 任意のConfigファイルに移行して参照するのが良さそう
	public static Dictionary<string, Rect> Name2RectMap = new Dictionary<string, Rect>()
	{
		{ "thumbnail", new Rect(0, 0, 500, 500) },
		{ "face",      new Rect(0, 0, 800, 800) },
		{ "bustup",    new Rect(0, 0, 1500, 1600) }
	};

	// 上部メニュー "Assets/Sprite/Separate" より呼び出せる様にする
	[MenuItem("Assets/Sprite/Separate")]
	public static void SeparateSprite()
	{
		// Selection.objects で現在Projectビューで選択しているファイル群が指定出来る
		IEnumerable<Texture> targets = Selection.objects.OfType<Texture>();
		if (!targets.Any())
		{
			Debug.LogWarning ("Please selecting textures.");
			return;
		}

		foreach (Texture target in targets)
		{
			Separate(AssetDatabase.GetAssetPath(target));
		}
	}

	public static void Separate(string texturePath)
	{
		TextureImporter importer = TextureImporter.GetAtPath(texturePath) as TextureImporter;

		// Update textureType
		importer.textureType = TextureImporterType.Sprite;
		importer.spriteImportMode = SpriteImportMode.Multiple;
		importer.filterMode = FilterMode.Point;
		EditorUtility.SetDirty(importer);
		AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceUpdate);

		// spriteImportMode separate
		SpriteMetaData[] sprites = Name2RectMap.Keys.Select(
			name => new SpriteMetaData
			{
				name = name,
				rect = Name2RectMap[name]
			}
		).ToArray();

		importer.spritesheet = sprites;
		EditorUtility.SetDirty(importer);
		AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceUpdate);
	}
}


