using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class flashImage : MonoBehaviour
{
    Image _image = null;
    Coroutine currentFlashRoutine = null;
    // Start is called before the first frame update

    private bool isFlashing;

    void Start()
    {
        _image = GetComponent<Image>();
    }
    public void StartFlash(float secondsForOneFlash, float maxAlpha, Color newColor)
    {
        if(!isFlashing)
        {
            _image.color = newColor;
            //insure maxAlpha isnt above 1
            maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);
            if (currentFlashRoutine != null)
            {
                StopCoroutine(currentFlashRoutine);
            }
            currentFlashRoutine = StartCoroutine(flash(secondsForOneFlash, maxAlpha));
            var tempColor = _image.color;
            tempColor.a = 0f;
            _image.color = tempColor;
        }
<<<<<<< Updated upstream
=======
        currentFlashRoutine = StartCoroutine(flash(secondsForOneFlash, maxAlpha));
>>>>>>> Stashed changes
    }
    IEnumerator flash(float secondsForOneFlash, float maxAlpha)
    {
        isFlashing = true;
        //animate flash in
        float flashInDuration = secondsForOneFlash / 2;
        for (float i = 0; i <= flashInDuration; i+= Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0, maxAlpha, i/flashInDuration);
            _image.color = colorThisFrame;
            yield return null; 
        }
        //animate flash outs
        for (float i = 0; i <= flashInDuration; i+=Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(maxAlpha, 0, i / flashInDuration);
            _image.color = colorThisFrame;
            yield return null;
        }
<<<<<<< Updated upstream
        
        isFlashing = false;
=======

        Color temp = _image.color;
        temp.a = 0f;
        _image.color = temp;

>>>>>>> Stashed changes
    }
}
