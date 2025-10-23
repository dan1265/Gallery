using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour
{
    public string imageUrl; // Pega aquí el enlace directo
    public Image targetImage; // Si usas UI
    public string videoUrl; // Pega aquí el enlace directo del video

    public LoadVideoManager videoManager; // Referencia al LoadVideoManager
    private void Awake()
    {
        if (videoManager == null)
        {
            videoManager = FindObjectOfType<LoadVideoManager>();
        }
    }
    void Start()
    {
        if (string.IsNullOrEmpty(imageUrl))
            imageUrl = "https://drive.google.com/uc?export=download&id=1B00i4L89y0T4poLns4MUiMY_wuSUUyg8";

        StartCoroutine(DownloadImage());
    }

    IEnumerator DownloadImage()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {

            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite newSprite = Sprite.Create(texture, new Rect(0.0f,0.0f, texture.width,texture.height), new Vector2(0.5f,0.5f), 100.0f);
            if (targetImage != null)
                targetImage.sprite = newSprite;
        }
        else
        {
            Debug.Log("Error al descargar imagen: " + request.error);
        }
    }

    public void PlayVideo()
    {
        if(!string.IsNullOrEmpty(videoUrl))
            if (videoManager != null)
                videoManager.PlayVideo(videoUrl);
    }
}