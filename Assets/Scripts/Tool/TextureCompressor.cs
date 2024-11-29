//using UnityEngine;
//using UnityEditor;

//public class TextureCompressor : EditorWindow
//{
//    [MenuItem("Tools/Texture Compressor")]
//    public static void ShowWindow()
//    {
//        GetWindow<TextureCompressor>("Texture Compressor");
//    }

//    private void OnGUI()
//    {
//        if (GUILayout.Button("Compress Textures for Android"))
//        {
//            CompressTextures();
//        }
//    }

//    private static void CompressTextures()
//    {
//        string[] guids = AssetDatabase.FindAssets("t:Texture2D");

//        int processedCount = 0; 
//        foreach (string guid in guids)
//        {
//            string path = AssetDatabase.GUIDToAssetPath(guid);

//            if (path.StartsWith("Packages"))
//            {
//                continue;
//            }

//            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
//            if (importer == null)
//                continue;

//            TextureImporterPlatformSettings androidSettings = importer.GetPlatformTextureSettings("Android");

//            bool isCrunched = androidSettings.format == TextureImporterFormat.ETC2_RGBA8Crunched;
//            bool isASTC = androidSettings.format == TextureImporterFormat.ASTC_8x8;

//            if (isCrunched || isASTC)
//            {
//                continue; 
//            }

//            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
//            if (texture == null)
//                continue;

//            int width = texture.width;
//            int height = texture.height;

//            androidSettings.overridden = true;
//            if (width % 4 == 0 && height % 4 == 0)
//            {
//                androidSettings.format = TextureImporterFormat.ETC2_RGBA8Crunched; // RGBA Crunched ETC2
//            }
//            else
//            {
//                androidSettings.format = TextureImporterFormat.ASTC_8x8; // RGBA Compressed ASTC 8x8
//            }

//            importer.SetPlatformTextureSettings(androidSettings);

//            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//            processedCount++;
//        }

//        Debug.Log($"Texture compression complete! Total textures processed: {processedCount}");
//    }
//}