﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Effekseer
{
	public enum EffekseerRendererType
	{
		Native = 0,
		Unity = 1,
	}

	public class EffekseerSettings : ScriptableObject
	{
		/// <summary xml:lang="en">
		/// Whether it does draw in scene view for editor.
		/// </summary>
		/// <summary xml:lang="ja">
		/// エディタのシーンビューに描画するかどうか
		/// </summary>
		[SerializeField]
		public bool drawInSceneView = true;

		/// <summary>
		/// 
		/// </summary>
		[SerializeField]
		public EffekseerRendererType RendererType = EffekseerRendererType.Unity;

		/// <summary xml:lang="en">
		/// Maximum number of effect instances.
		/// </summary>
		/// <summary xml:lang="ja">
		/// エフェクトインスタンスの最大数
		/// </summary>
		[SerializeField]
		public int effectInstances	= 8192;

		/// <summary xml:lang="en">
		/// Maximum number of quads that can be drawn.
		/// </summary>
		/// <summary xml:lang="ja">
		/// 描画できる四角形の最大数
		/// </summary>
		[SerializeField]
		public int maxSquares		= 8192;

		/// <summary xml:lang="en">
		/// The coordinate system of effects.
		/// if it is true, effects is loaded as same as before version 1.3.
		/// if it is false, effects is shown as same as the editor.
		/// </summary>
		/// <summary xml:lang="ja">
		/// エフェクトの座標系
		/// trueならば、version1.3以前と同じように読み込まれる。
		/// falseならば、エディタと同じように表示される。
		/// </summary>
		[SerializeField]
		public bool isRightEffekseerHandledCoordinateSystem = false;

		/// <summary xml:lang="en">
		/// Maximum number of sound instances.
		/// </summary>
		/// <summary xml:lang="ja">
		/// サウンドインスタンスの最大数
		/// </summary>
		[SerializeField]
		public int soundInstances	= 16;

		/// <summary xml:lang="en">
		/// Enables distortion effect.
		/// When It has set false, rendering will be faster.
		/// </summary>
		/// <summary xml:lang="ja">
		/// 歪みエフェクトを有効にする。
		/// falseにすると描画処理が軽くなります。
		/// </summary>
		[SerializeField]
		public bool enableDistortion	= true;

		[SerializeField]
		public Shader standardShader = null;

		[SerializeField]
		public Shader standardDistortionShader = null;

		private static EffekseerSettings instance;
		public static EffekseerSettings Instance {
			get {
				if (instance != null) {
					return instance;
				}
				instance = Resources.Load<EffekseerSettings>("EffekseerSettings");
				if (instance == null) {
					instance = new EffekseerSettings();
				}
				return instance;
			}
		}

#if UNITY_EDITOR
		[MenuItem("Edit/Project Settings/Effekseer")]
		public static void EditOrCreateAsset()
		{
			const string assetDir = "Assets/Effekseer";
			const string materialDir = assetDir + "/Materials";
			const string resourcesDir = assetDir + "/Resources";
			const string assetPath = resourcesDir + "/EffekseerSettings.asset";

			if (!AssetDatabase.IsValidFolder(resourcesDir)) {
				AssetDatabase.CreateFolder(assetDir, "Resources");
			}
			var asset = AssetDatabase.LoadAssetAtPath<EffekseerSettings>(assetPath);

			if (asset == null) {
				asset = CreateInstance<EffekseerSettings>();
				asset.standardShader = AssetDatabase.LoadAssetAtPath<Shader>(materialDir + "/StandardShader.shader");
				asset.standardDistortionShader = AssetDatabase.LoadAssetAtPath<Shader>(materialDir + "/StandardDistortionShader.shader");
				AssetDatabase.CreateAsset(asset, assetPath);
				AssetDatabase.Refresh();
			}

			EditorGUIUtility.PingObject(asset);
			Selection.activeObject = asset;
		}
#endif
	}
}
