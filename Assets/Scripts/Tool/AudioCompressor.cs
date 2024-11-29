//using UnityEngine;
//using UnityEditor;

//public class AudioCompressor : EditorWindow
//{
//    [MenuItem("Tools/Audio Compressor")]
//    public static void ShowWindow()
//    {
//        GetWindow<AudioCompressor>("Audio Compressor");
//    }

//    private void OnGUI()
//    {
//        if (GUILayout.Button("Compress Audio Clips"))
//        {
//            CompressAudioClips();
//        }
//    }

//    private static void CompressAudioClips()
//    {
//        // Retrieve all AudioClips in the project
//        string[] guids = AssetDatabase.FindAssets("t:AudioClip");

//        foreach (string guid in guids)
//        {
//            string path = AssetDatabase.GUIDToAssetPath(guid);

//            // Load the audio importer
//            AudioImporter importer = AssetImporter.GetAtPath(path) as AudioImporter;
//            if (importer == null)
//                continue;

//            // Load the AudioClip to get its length
//            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
//            if (clip == null)
//                continue;

//            // Configure settings for the Android platform
//            AudioImporterSampleSettings androidSettings = importer.GetOverrideSampleSettings("Android");

//            if (clip.length < 5f) // Short audio (less than 5 seconds)
//            {
//                androidSettings.loadType = AudioClipLoadType.DecompressOnLoad;
//                androidSettings.compressionFormat = AudioCompressionFormat.ADPCM; // Or PCM if needed
//                Debug.Log($"Compressed (short): {path} | Format: {androidSettings.compressionFormat} | LoadType: {androidSettings.loadType}");
//            }
//            else // Long audio (5 seconds or more)
//            {
//                androidSettings.loadType = AudioClipLoadType.Streaming;
//                androidSettings.compressionFormat = AudioCompressionFormat.Vorbis;
//                Debug.Log($"Compressed (long): {path} | Format: {androidSettings.compressionFormat} | LoadType: {androidSettings.loadType}");
//            }

//            // Apply changes for the Android platform
//            importer.SetOverrideSampleSettings("Android", androidSettings);

//            // **Important:** Mark the file as dirty
//            EditorUtility.SetDirty(importer);

//            // Reimport to apply changes
//            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//        }

//        Debug.Log("Audio compression complete!");
//    }
//}
