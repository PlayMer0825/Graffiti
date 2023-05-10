using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureDivide : MonoBehaviour {
    public Texture2D sourceTex;

    public Rect sourceRect;

    public Image _alpha = null;
    public Image _rgb = null;

    [SerializeField] Texture2D alphatexture = null;
    [SerializeField] Texture2D rgbtexture = null;

    private void Awake() {
        TextureRgbA(sourceTex);
    }

    void TextureRgbA(Texture2D texture) {

        var tw = texture.width;
        var th = texture.height;
        var source = texture.GetPixels();
        var pixels = texture.GetPixels();
        var alphapixels = texture.GetPixels();

        Debug.Log($"source Counts: {source.Length}");

        var i1 = 0;
        var i2 = 0;

        int alphaPixelCount = 0;

        for(var iy = 0; iy < th; iy++) {
            for(var ix = 0; ix < tw; ix++) {
                alphapixels[i2++] = new Color(0, 0, 0, source[i1++].a);
                if(source[i1 - 1].a >= 0.9f)
                    alphaPixelCount++;
            }
        }

        Debug.Log($"Alpha Counts: {alphaPixelCount}");
        alphatexture = new Texture2D(tw, th);
        alphatexture.SetPixels(alphapixels);

        i2 = i1 = 0;

        for(var iy = 0; iy < th; iy++) {
            for(var ix = 0; ix < tw; ix++) {
                pixels[i2++] = source[i1++];
            }
        }

        rgbtexture = new Texture2D(tw, th);
        rgbtexture.SetPixels(pixels);

        alphatexture.Apply();
        rgbtexture.Apply();

        _alpha.sprite = Sprite.Create(alphatexture, new Rect(0, 0, alphatexture.width, alphatexture.height), new Vector3(0.5f, 0.5f));
        _rgb.sprite = Sprite.Create(rgbtexture, new Rect(0, 0, rgbtexture.width, rgbtexture.height), new Vector3(0.5f, 0.5f));
    }
}
