using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ClosingScript : MonoBehaviour
{
    private string[] text;
    private int textNumber;
    private string cityName = "Jakarta", cityTreasure = "Super Semar", cityTreasurePlace = "Museum Nasional", criminalName = "PandjiPragiwaksono";
    public Transform carmen, plane;
    public GameObject winObj, loseObj, obj2, obj3;
    public Button nextButton;
    public TextMeshProUGUI descText;
    private int isNext;
    private GameObject GRAPH;
    private Graph graphScript;
    public AudioSource winSound, loseSound, typingSound;
    // Start is called before the first frame update

    void Start()
    {
        //text = new string[5];
        textNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTreasure(string str)
    {

    }

    public void Closing(int endingState)
    {
        GRAPH = GameObject.Find("GRAPH");
        graphScript = GRAPH.GetComponent<Graph>();
        criminalName = graphScript.getCriminal().Name;
        isNext = 0;
        winObj.SetActive(false);
        loseObj.SetActive(false);
        obj2.SetActive(false);
        obj3.SetActive(false);
        text = new string[5];
        textNumber = 0;
        if (endingState == 1)
        {
            print("MASUK PAK EKO");
            text[0] = "Kabar Kasus Terbaru !!";
            text[1] = "Pelaku Kasus Pencurian " + cityTreasure + " Berhasil Ditangkap !!!";
            text[2] = "Pelaku Bernama <b>" + criminalName + "</b> berhasil ditangkap oleh kepolisian setempat.";
            text[3] = "Ditemukan " + cityTreasure + " pada salah satu barang bawaan pelaku ketika hendak meninggalkan bandara.";
            text[4] = "Kerja bagus detektif! Anda berhasil menangkap pelaku!";
            //text[5] = "Apakah anda ingin menyelesaikan kasus selanjutnya?";
            isNext = 1;
        }
        else if (endingState == -1)
        {
            print("MASUK PAK EKO");
            text[0] = "Kabar Kasus Terbaru !!";
            text[1] = "Tersangka Kasus Pencurian " + cityTreasure + " Berhasil Ditangkap !!!";
            text[2] = "Namun, tidak ditemukan " + cityTreasure + " pada barang bawaan tersangka ketika diperiksa pihak setempat.";
            text[3] = "Diduga terjadi salah tangkap terhadap tersangka dan pelaku melarikan diri ke luar negeri.";
            text[4] = "Sayang sekali detektif! Anda gagal menangkap pelaku dengan tepat!";
            //text[5] = "Apakah anda ingin mencoba kasus selanjutnya?";
        }
        else if (endingState == -2)
        {
            print("MASUK PAK EKO");
            text[0] = "Kabar Kasus Terbaru !!";
            text[1] = "Tersangka Kasus Pencurian " + cityTreasure + " Gagal Ditangkap !!!";
            text[2] = "Ditemukan " + cityTreasure + " pada barang bawaan pelaku oleh pihak bandara.";
            text[3] = "Namun, karena tidak adanya laporan membuat pelaku dapat pergi ke luar negeri dengan barang curiannya.";
            text[4] = "Sayang sekali detektif! Anda gagal menangkap pelaku dengan cepat!";
            //text[5] = "Apakah anda ingin mencoba kasus selanjutnya?";
        }
        print(endingState);
        StartCoroutine(PlayAnim(endingState));
    }

    public int getIsNext()
    {
        return isNext;
    }

    IEnumerator PlayAnim(int endingState)
    {
        print(text.Length + " Adalah Ukuran Array " + text[0]);
        yield return new WaitForSeconds(4.5f);
        if (endingState > 0)
        {
            winObj.SetActive(true);
            winObj.transform.localScale = Vector2.zero;
            winObj.LeanScale(Vector2.one, 1.25f);
            winObj.LeanRotateZ(0f, 1.25f);
            winSound.Play();
            yield return new WaitForSeconds(3.0f);
            winObj.SetActive(false);
        }
        else
        {
            loseObj.SetActive(true);
            plane.localPosition = new Vector2(160, 0);
            plane.LeanMoveLocalX(-160, 3.75f);
            loseSound.Play();
            yield return new WaitForSeconds(4.0f);
            loseObj.SetActive(false);
        }
        obj2.SetActive(true);
        SetButtonState(0);
        print(textNumber);
        print(text[0]);
        print(text[textNumber]);
        StartCoroutine(PlayText(text[textNumber]));
    }

    IEnumerator PlayText(string str)
    {
        typingSound.Play();
        descText.text = "";
        foreach (char c in str)
        {
            descText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        typingSound.Stop();
        SetButtonState(1);
    }

    private void SetButtonState(int i)
    {
        Color temp = nextButton.image.color;
        if (i == 0)
        {
            nextButton.enabled = false;
            temp.a = 0.2f;
            nextButton.image.color = temp;
        }
        else
        {
            nextButton.enabled = true;
            temp.a = 1f;
            nextButton.image.color = temp;
        }
    }

    public void NextDesc()
    {
        textNumber += 1;
        if (textNumber == text.Length)
        {
            obj2.SetActive(false);
            obj3.SetActive(true);
        }
        else
        {
            SetButtonState(0);
            StartCoroutine(PlayText(text[textNumber]));
        }
    }

    public void setName(string cityTreasure)
    {
        this.cityTreasure = cityTreasure;
    }

    public void setNameCrim()
    {

    }
}
