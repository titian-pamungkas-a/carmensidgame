using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZzAnim : MonoBehaviour
{
    //public Image bgImage;
    public CanvasGroup cg;
    public GameObject bgImage;
    void Start()
    {
        bgImage.SetActive(false);
        StartCoroutine(bgAnim());
    }

    IEnumerator bgAnim()
    {
        bgImage.SetActive(true);
        cg.alpha = 0;
        cg.LeanAlpha(1, 1f);
        yield return new WaitForSeconds(2.0f);
        cg.LeanAlpha(0, 1f);
        yield return new WaitForSeconds(1.0f);
        bgImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
