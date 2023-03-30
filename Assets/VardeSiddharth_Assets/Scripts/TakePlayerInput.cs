using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePlayerInput : MonoBehaviour
{
    public List<Group> playerCreatedGroups;

    Group currentCreatedGroup;

    int maxRows = 5, maxColumns = 5;
    int currentActiveRow = 0, currentActiveColumn = 0;
    int lastActiveRow = -1, lastActiveColumn = -1;
    bool canTakeInput = true;


    // Start is called before the first frame update
    void Start()
    {
        currentCreatedGroup = new Group();
    }

    // Update is called once per frame
    void Update()
    {
        if (canTakeInput)
        {
            TakeInput();
            ChangeCurrentActiveImage(currentActiveRow, currentActiveColumn);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                string data = currentActiveRow.ToString() + currentActiveColumn.ToString();
                int removeFromIndex = -1;
                for (int i = 0; i < currentCreatedGroup.group.Count; i++)
                {
                    if (data == currentCreatedGroup.group[i])
                    {
                        removeFromIndex = i;
                    }
                }

                if (removeFromIndex != -1)
                {
                    currentCreatedGroup.group.RemoveAt(removeFromIndex);
                }
                else
                {
                    currentCreatedGroup.group.Add(data);
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                playerCreatedGroups.Add(currentCreatedGroup);
                //Color color = new Color(Random.Range(0.5f, 0.9f), Random.Range(0.5f, 0.9f), Random.Range(0.5f, 0.9f));
                //foreach(string a in currentCreatedGroup.group)
                //{
                //    int row = int.Parse(a[0].ToString());
                //    int column = int.Parse(a[1].ToString());

                //    transform.GetChild(row).GetChild(column).GetComponent<Image>().color = color;
                //}
                currentCreatedGroup = new Group();
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            canTakeInput = false;
            GenerateGroups generateGroupsRefrence = GetComponent<GenerateGroups>();

            for(int i = 0; i < playerCreatedGroups.Count; i++)
            {
                bool isCorrect = true;

                for (int j = 0; j < generateGroupsRefrence.generatedGroups.Count; j++)
                {
                    if(generateGroupsRefrence.generatedGroups[j].group.Contains(playerCreatedGroups[i].group[0]) 
                        && generateGroupsRefrence.generatedGroups[j].group.Count == playerCreatedGroups[i].group.Count)
                    {
                        //Debug.Log("Will change Color");
                        foreach(string x in playerCreatedGroups[i].group)
                        {
                            //Debug.Log("Inside Foreach Loop that checks for every element x is " + x);

                            if(generateGroupsRefrence.generatedGroups[j].group.Contains(x) == false)
                            {
                                isCorrect = false;
                            }
                        }
                        //Debug.Log("Is Corrects Value is " + isCorrect);

                        if(isCorrect)
                        {
                            Color color = new Color(Random.Range(0.5f, 0.9f), Random.Range(0.5f, 0.9f), Random.Range(0.5f, 0.9f));
                            //Debug.Log("Going To change Color");
                            foreach (string a in playerCreatedGroups[i].group)
                            {
                                //Debug.Log("Inside Foreach");
                                int row = int.Parse(a[0].ToString());
                                int column = int.Parse(a[1].ToString());

                                transform.GetChild(row).GetChild(column).GetComponent<Image>().color = color;
                                //Debug.Log("Changing Color");
                            }
                        }
                    }
                }

                //if(isCorrect == false)
                //{
                //    foreach (string a in playerCreatedGroups[i].group)
                //    {
                //        //Debug.Log("Inside Foreach");
                //        int row = int.Parse(a[0].ToString());
                //        int column = int.Parse(a[1].ToString());

                //        transform.GetChild(row).GetChild(column).GetComponent<Image>().color = Color.red;
                //        //Debug.Log("Changing Color");
                //    }
                //}
            }
        }
    }

    public void TakeInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            lastActiveRow = currentActiveRow;
            lastActiveColumn = currentActiveColumn;
            currentActiveRow++;
            if (currentActiveRow >= maxRows)
            {
                currentActiveRow = maxRows - 1;
                lastActiveRow = currentActiveRow - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lastActiveRow = currentActiveRow;
            lastActiveColumn = currentActiveColumn;
            currentActiveRow--;
            if (currentActiveRow < 0)
            {
                currentActiveRow = 0;
                lastActiveRow = currentActiveRow + 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lastActiveColumn = currentActiveColumn;
            lastActiveRow = currentActiveRow;
            currentActiveColumn++;
            if (currentActiveColumn >= maxColumns)
            {
                currentActiveColumn = maxColumns - 1;
                lastActiveColumn = currentActiveColumn - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lastActiveColumn = currentActiveColumn;
            lastActiveRow = currentActiveRow;
            currentActiveColumn--;
            if (currentActiveColumn < 0)
            {
                currentActiveColumn = 0;
                lastActiveColumn = currentActiveColumn + 1;
            }
        }
    }

    public void ChangeCurrentActiveImage(int row, int column)
    {
        transform.GetChild(row).GetChild(column).GetComponent<Image>().color = Color.red;
        if(lastActiveColumn != -1 && lastActiveRow != -1)
        {
            transform.GetChild(lastActiveRow).GetChild(lastActiveColumn).GetComponent<Image>().color = Color.white;
        }
    }
}
