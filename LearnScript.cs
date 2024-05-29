using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LearnScript : MonoBehaviour
{
    private GameObject mainManager, textObject;
    public GameObject detailObject;
    public GameObject buttonPrefabs;
    private MainManager script;
    public TMP_Dropdown dd;
    private List<string> listCities;
    private List<TMP_Dropdown.OptionData> dropdownList;
    private List<AttributeCity> attributeCities;
    public Image bgImage, bgTitle;
    private List<Attribute> attributes;
    public TextMeshProUGUI clueDesc, sceneTitle, detailCity;
    private int currentItem;
    private RectTransform rectTransform;
    public ContentSizeFitter csf;
    private RectTransform rt;
    void Start()
    {
        currentItem = 0;
        //detailObject = GameObject.Find("DetailUI");
        mainManager = GameObject.Find("MainManager");
        script = mainManager.GetComponent<MainManager>();
        listCities = new List<string>();
        dropdownList = new List<TMP_Dropdown.OptionData>();
        attributeCities = new List<AttributeCity>(script.GetAttributeCities());
        attributes = new List<Attribute>(script.GetAttributes());
        for (int i = 0; i < attributeCities.Count;i++)
        {
            //listCities.Add(attributeCities[i].Name);
            var asd = new TMP_Dropdown.OptionData(attributeCities[i].Name);
            dropdownList.Add(asd);
        }
        dd.ClearOptions();
        dd.AddOptions(dropdownList);
        dd.value = 0;
        bgImage.sprite = Resources.Load<Sprite>("Background/" + attributeCities[dd.value].ImagePath);
        dd.onValueChanged.AddListener(delegate
        {
            valueChanged(dd);
        });
        sceneTitle.text = attributeCities[dd.value].Name;
        bgTitle.rectTransform.sizeDelta = new Vector2(80 + attributeCities[dd.value].Name.Length*22, 60);
        check(dd, currentItem);
        createDescription();
        
    }

    private void createDescription()
    {
        //print("Generate1");
        float y = 480;
        int x = 0;
        deleteDesc();
        for (int i = 0; i < attributes.Count; i++)
        {
            //print("Generate " + i);
            createText(new Vector3(-20, y, 0), attributes[i].Name);
            y -= 30;
            x += 1;
            for (int j = 0; j < attributeCities[dd.value].Clue[i].Count; j++)
            {
                createText(new Vector3(-10, y, 0), attributeCities[dd.value].Clue[i][j]);
                y -= 30;
                x += 1;
            }if (attributeCities[dd.value].Clue[i].Count == 0)
            {
                createText(new Vector3(-10, y, 0), "--");
                y -= 30;
            }

            y -= 20;
            x += 1;
        }
        detailCity.text = attributeCities[dd.value].Detail;

        /*rt.sizeDelta = new Vector2(250, Mathf.Abs(y));*/
    }

    private void check(TMP_Dropdown dd, int num)
    {
        //deleteDesc();
        //clueTitle.text = attributes[currentItem].Name;
        /*if (attributeCities[dd.value].Clue[num].Count == 0)
        {
            createText(new Vector3(175, -20, 0), "-");
        }
        else
        {
            //clueDesc.text = attributeCities[dd.value].Clue[num][0];
            for (int i = 0; i < attributeCities[dd.value].Clue[num].Count; i++)
            {
                createText(new Vector3(175, -20 - 40*i, 0), attributeCities[dd.value].Clue[num][i]);
            }
        }*/
    }

    private void createText(Vector3 pos, string str)
    {
        print(str + " Berhasil diprint");
        textObject = Object.Instantiate(buttonPrefabs, Vector3.zero, Quaternion.identity);
        clueDesc = textObject.GetComponent<TextMeshProUGUI>();
        rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.SetParent(detailObject.transform);
        rectTransform.localScale = new Vector3(1, 1, 1);
        rectTransform.localPosition = pos;
        clueDesc.text = str;
    }

    void valueChanged(TMP_Dropdown dd)
    {
        print(attributeCities[dd.value].Name);
        bgImage.sprite = Resources.Load<Sprite>("Background/" + attributeCities[dd.value].ImagePath);
        sceneTitle.text = attributeCities[dd.value].Name;
        bgTitle.rectTransform.sizeDelta = new Vector2(80 + attributeCities[dd.value].Name.Length * 22, 60);
        //check(dd, currentItem);
        createDescription();
        //deleteDesc();
    }

    public void leftButton()
    {
        currentItem -= 1;
        if (currentItem < 0)
            currentItem = attributes.Count - 1;
        check(dd, currentItem);
    }

    public void rightButton()
    {
        currentItem += 1;
        if (currentItem == attributes.Count)
            currentItem = 0;
        check(dd, currentItem);
    }

    private void deleteDesc()
    {
        foreach (Transform child in detailObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /*public void onChangeValue(Dropdown val)
    {
        //bgImage.sprite = Resources.Load<Sprite>("Background/" +  attributeCities[val.value].ImagePath);
        //print(attributeCities[val.value].Name);
        print(val.value);
    }*/


    void Update()
    {
        
    }
}
