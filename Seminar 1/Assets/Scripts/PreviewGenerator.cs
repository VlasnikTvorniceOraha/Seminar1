using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PreviewGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate Preview")]
    static void GeneratePreview()
    {
        GameObject targetObject = Selection.activeGameObject;
        if (targetObject == null)
        {
            Debug.LogWarning("No object selected.");
            return;
        }

        // Generate the preview
        Texture2D previewTexture = AssetPreview.GetAssetPreview(targetObject);

        // Wait until the preview is ready
        while (previewTexture == null)
        {
            System.Threading.Thread.Sleep(100); // Wait a bit
        }

        // Save the preview texture as an asset
        byte[] textureData = previewTexture.EncodeToPNG();
        string path = "Assets/Resources/Preview/" + targetObject.name + ".png";
        System.IO.File.WriteAllBytes(path, textureData);
        AssetDatabase.ImportAsset(path);

        Debug.Log("Preview saved at: " + path);
    }
}