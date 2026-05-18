using System.IO;
using UnityEngine;

namespace Ygo.Service
{
    public static class ImageLoader
    {
        public static Sprite LoadSpriteFromFile(uint cardCode, bool small)
        {
            var smallPath = small ? "-small" : "";
            var filePath = Path.Combine(Application.streamingAssetsPath, $"cards/imgs{smallPath}/{cardCode}.jpg");
            
            
            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"Imagem não encontrada no caminho: {filePath}");
                return null;
            }

            byte[] fileData = File.ReadAllBytes(filePath);
        
            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        
            if (texture.LoadImage(fileData))
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                return sprite;
            }

            return null;
        }
        
        public static void UnloadSprite(Sprite sprite)
        {
            if (sprite == null) return;

            if (sprite.texture != null)
            {
                Object.Destroy(sprite.texture);
            }
            Object.Destroy(sprite);
        }
    }
}