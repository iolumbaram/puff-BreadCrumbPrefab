using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BreadCrumb : MonoBehaviour
{
    public Button NextButton = null;
    public Button PrevButton = null;

    public Transform BreadCrumbList = null;
    public Transform BreadCrumbTR = null;

    public Stack BreadCrumbStack = null;

    void Start()
    {
        NextButton.onClick.AddListener(() => OnClickNextButton());
        PrevButton.onClick.AddListener(() => OnClickPrevBUtton());

        BreadCrumbStack = new Stack();
    }

    void Update()
    {
        
    }

    public void OnClickNextButton()
    {
        int currentStackIndex = BreadCrumbStack.Count;
        GameObject newBreadCrumbGO = Instantiate(BreadCrumbTR.gameObject, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        newBreadCrumbGO.name = (currentStackIndex + 1).ToString();
        newBreadCrumbGO.AddComponent<Button>();
        
        foreach (Transform child in newBreadCrumbGO.transform)
        {
            if (child.name == "Text")
            {
                child.GetComponent<Text>().text = "Page " + (currentStackIndex + 1).ToString();
            }
        }

        newBreadCrumbGO.GetComponent<Button>().onClick.AddListener(() => JumpToPrevPagebyX(int.Parse(newBreadCrumbGO.name)));
        BreadCrumbStack.Push(newBreadCrumbGO);
        UpdateBreadCrumbListFrom(BreadCrumbStack);
    }

    public void OnClickPrevBUtton()
    {
        try
        {
            Destroy((GameObject)BreadCrumbStack.Pop());
            UpdateBreadCrumbListFrom(BreadCrumbStack);
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    private void UpdateBreadCrumbListFrom(IEnumerable stackCollection)
    {
        foreach (GameObject breadCrumbGO in stackCollection)
        {
            breadCrumbGO.transform.SetParent(BreadCrumbList.transform, false);
            breadCrumbGO.SetActive(true);
            string name = breadCrumbGO.name;
        }
    }

    private void JumpToPrevPagebyX(int stackIndex)
    {
        for(var i= BreadCrumbStack.Count; i>stackIndex; i--)
        {
            OnClickPrevBUtton();
        }
    }

    private void OnDestroy()
    {
        BreadCrumbStack.Clear();
    }
}
