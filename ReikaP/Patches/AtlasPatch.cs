using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using System.IO;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public string modsFolder = "image/Skins"; // assetstream + image / Skins 
    public SkeletonAnimation skeletonAnim;

    private static SkinManager _instance;
    public static SkinManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SkinManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (skeletonAnim == null)
        {
            skeletonAnim = GetComponent<SkeletonAnimation>();
            if (skeletonAnim == null)
            {
                Debug.LogError("SkeletonAnimation component not found.");
            }
        }
    }



    public void ApplyCustomSkin(string textureFileName, string slotName, string attachmentName, SkeletonAnimation skeletonAnim)
    {
        if (skeletonAnim == null)
        {
            Debug.LogError("SkeletonAnimation is null.");
            return;
        }

        string modsPath = Path.Combine(Application.streamingAssetsPath, modsFolder).Replace("\\", "/");
        string texturePath = Path.Combine(modsPath, textureFileName).Replace("\\", "/");

        if (!File.Exists(texturePath))
        {
            Debug.LogWarning($"Texture file not found: {texturePath}");
            return;
        }

        // Load the texture
        Texture2D customTexture = LoadTexture(texturePath);
        if (customTexture == null)
        {
            Debug.LogWarning($"Failed to load texture from: {texturePath}");
            return;
        }

        // Create a material for the texture
        Material baseMaterial = skeletonAnim.SkeletonDataAsset.atlasAssets[0].PrimaryMaterial;
        Material material = new Material(baseMaterial);
        material.mainTexture = customTexture;

        // Create an AtlasRegion from the Texture2D
        AtlasRegion atlasRegion = CreateAtlasRegion(customTexture, material, textureFileName);

        if (atlasRegion == null)
        {
            Debug.LogError("Failed to create AtlasRegion from texture.");
            return;
        }

        float scale = skeletonAnim.SkeletonDataAsset.scale; // Use the skeleton's scale

        // Create a RegionAttachment from the AtlasRegion
        RegionAttachment attachment = new RegionAttachment(attachmentName);
        attachment.SetRegion(atlasRegion);
        attachment.Path = attachmentName;
        attachment.ScaleX = 1f;
        attachment.ScaleY = 1f;
        attachment.RegionWidth = atlasRegion.width;
        attachment.RegionHeight = atlasRegion.height;
        attachment.RegionOriginalWidth = atlasRegion.originalWidth;
        attachment.RegionOriginalHeight = atlasRegion.originalHeight;
        attachment.RegionOffsetX = atlasRegion.offsetX;
        attachment.RegionOffsetY = atlasRegion.offsetY;
        attachment.Width = attachment.RegionOriginalWidth * scale;
        attachment.Height = attachment.RegionOriginalHeight * scale;
        attachment.Rotation = 0f;
        attachment.X = 0f;
        attachment.Y = 0f;

        // Set UVs
        attachment.SetUVs(atlasRegion.u, atlasRegion.v, atlasRegion.u2, atlasRegion.v2, atlasRegion.rotate);

        // Update the attachment's offset
        attachment.UpdateOffset();

        // Create a new skin and add the attachment
        Skin customSkin = new Skin("customSkin");
        int slotIndex = skeletonAnim.Skeleton.FindSlotIndex(slotName);
        customSkin.SetAttachment(slotIndex, attachmentName, attachment);

        // Set the new skin on the skeleton
        skeletonAnim.Skeleton.SetSkin(customSkin);
        skeletonAnim.Skeleton.SetSlotsToSetupPose();
        skeletonAnim.AnimationState.Apply(skeletonAnim.Skeleton);

        Debug.Log($"Custom skin '{textureFileName}' applied to slot '{slotName}'.");
    }


    private AtlasRegion CreateAtlasRegion(Texture2D texture, Material material, string regionName)
    {
        AtlasPage page = new AtlasPage
        {
            name = regionName,
            width = texture.width,
            height = texture.height,
            rendererObject = material
        };

        AtlasRegion region = new AtlasRegion
        {
            page = page,
            name = regionName,
            index = -1,
            rotate = false,
            u = 0f,
            v = 1f,
            u2 = 1f,
            v2 = 0f,
            width = texture.width,
            height = texture.height,
            originalWidth = texture.width,
            originalHeight = texture.height,
            offsetX = 0f,
            offsetY = 0f
        };

        return region;
    }

    private Texture2D LoadTexture(string path)
    {
        try
        {
            byte[] fileData = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            if (tex.LoadImage(fileData))
            {
                tex.Apply();
                tex.name = Path.GetFileNameWithoutExtension(path);
                return tex;
            }
            else
            {
                Debug.LogError($"Failed to load texture from path: {path}");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error loading texture: {ex.Message}");
        }
        return null;
    }
}
