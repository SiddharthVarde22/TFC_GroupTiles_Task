using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateGroups : MonoBehaviour
{
    public List<string> occupiedCells;
    public List<Group> generatedGroups;

    int maxRows = 5, maxColumns = 5;
    int currentRow = 0, currentColumn = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (currentRow = 0; currentRow < maxRows; currentRow++)
        {
            for (currentColumn = 0; currentColumn < maxColumns; currentColumn++)
            {
                if (CheckIfCellIsOccupied(currentRow, currentColumn) == false)
                {
                    int numberOfCellsToGroup = Random.Range(2, 6);
                    //Debug.Log("Number Of Cells To Occupie " + numberOfCellsToGroup);
                    int direction = Random.Range(0, 2);
                    Group currentGroup = new Group();

                    if (direction == 0)
                    {
                        //occupie Cells On Right
                        int emptyCells = GetNumberOfEmptyCellsRight(currentRow, currentColumn);

                        if (numberOfCellsToGroup <= emptyCells)
                        {
                            //Color randomColor = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));

                            for (int i = 0; i < numberOfCellsToGroup; i++)
                            {
                                OccupieCell(currentRow, currentColumn + i);
                                currentGroup.group.Add(currentRow.ToString() + (currentColumn + i).ToString());
                                //transform.GetChild(currentRow).GetChild(currentColumn + i).GetComponent<Image>().color = randomColor;
                            }

                            transform.GetChild(currentRow).GetChild(currentColumn).GetChild(0).GetComponent<Text>().text = numberOfCellsToGroup.ToString();
                        }
                        else
                        {
                            int emptyCellsDownwards = GetNumberOfEmptyCellsBelow(currentRow, currentColumn);
                            //Color randomColor = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
                            if (emptyCells > 1)
                            {
                                for (int i = 0; i < emptyCells; i++)
                                {
                                    OccupieCell(currentRow, currentColumn + i);
                                    currentGroup.group.Add(currentRow.ToString() + (currentColumn + i).ToString());
                                    //transform.GetChild(currentRow).GetChild(currentColumn + i).GetComponent<Image>().color = randomColor;
                                }
                            }
                            else
                            {
                                if (numberOfCellsToGroup <= emptyCellsDownwards)
                                {
                                    emptyCells = numberOfCellsToGroup;
                                }
                                else
                                {
                                    emptyCells = emptyCellsDownwards;
                                }

                                for(int i = 0; i < emptyCells; i++)
                                {
                                    OccupieCell(currentRow + i, currentColumn);
                                    currentGroup.group.Add((currentRow + i).ToString() + currentColumn.ToString());
                                }
                            }

                            transform.GetChild(currentRow).GetChild(currentColumn).GetChild(0).GetComponent<Text>().text = emptyCells.ToString();
                        }
                    }
                    else if (direction == 1)
                    {
                        //occupie Cells Below
                        int emptyCells = GetNumberOfEmptyCellsBelow(currentRow, currentColumn);

                        if (numberOfCellsToGroup <= emptyCells)
                        {
                            //Color randomColor = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));

                            for (int i = 0; i < numberOfCellsToGroup; i++)
                            {
                                OccupieCell(currentRow + i, currentColumn);
                                currentGroup.group.Add((currentRow + i).ToString() + currentColumn.ToString());
                                //transform.GetChild(currentRow + i).GetChild(currentColumn).GetComponent<Image>().color = randomColor;
                            }
                            transform.GetChild(currentRow).GetChild(currentColumn).GetChild(0).GetComponent<Text>().text = numberOfCellsToGroup.ToString();
                        }
                        else
                        {
                            int emptyCellsOnRight = GetNumberOfEmptyCellsRight(currentRow, currentColumn);
                            //Color randomColor = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
                            if (emptyCells > 1)
                            {
                                for (int i = 0; i < emptyCells; i++)
                                {
                                    OccupieCell(currentRow + i, currentColumn);
                                    currentGroup.group.Add((currentRow + i).ToString() + currentColumn.ToString());
                                    //transform.GetChild(currentRow + i).GetChild(currentColumn).GetComponent<Image>().color = randomColor;
                                }
                            }
                            else
                            {
                                if(numberOfCellsToGroup <= emptyCellsOnRight)
                                {
                                    emptyCells = numberOfCellsToGroup;
                                }
                                else
                                {
                                    emptyCells = emptyCellsOnRight;
                                }

                                for(int i = 0; i < emptyCells; i++)
                                {
                                    OccupieCell(currentRow, currentColumn + i);
                                    currentGroup.group.Add(currentRow.ToString() + (currentColumn + i).ToString());
                                }
                            }
                            transform.GetChild(currentRow).GetChild(currentColumn).GetChild(0).GetComponent<Text>().text = emptyCells.ToString();
                        }
                    }

                    generatedGroups.Add(currentGroup);
                }
            }
        }

        for(int i = 0; i < generatedGroups.Count; i++)
        {
            if(generatedGroups[i].group.Count == 1)
            {
                string data = generatedGroups[i].group[0];
                int row = int.Parse(data[0].ToString());
                int column = int.Parse(data[1].ToString());
                bool isAdded = false;
                //Debug.Log("Found Single At " + i);

                for(int j = 0; j < generatedGroups.Count; j++)
                {
                    if (generatedGroups[j].group.Contains((row - 1).ToString() + column.ToString()))
                    {
                        //int secondUpperCellRow = row - 2;
                        if (generatedGroups[j].group.Contains((row - 2).ToString() + column.ToString()))
                        {
                            generatedGroups.RemoveAt(i);
                            i--;
                            generatedGroups[j].group.Add(row.ToString() + column.ToString());
                            string firstCellData = generatedGroups[j].group[0];
                            int firstCellRow = int.Parse(firstCellData[0].ToString());
                            int firstCellColumn = int.Parse(firstCellData[1].ToString());

                            transform.GetChild(firstCellRow).GetChild(firstCellColumn).GetChild(0).GetComponent<Text>().text
                                = generatedGroups[j].group.Count.ToString();

                            transform.GetChild(row).GetChild(column).GetChild(0).GetComponent<Text>().text = null;
                            isAdded = true;
                            //Debug.Log("Added To Upper Group  row " + row + " column " + column);
                        }
                    }
                }

                if(isAdded == false)
                {
                    for (int j = 0; j < generatedGroups.Count; j++)
                    {
                        if (generatedGroups[j].group.Contains(row.ToString() + (column - 1).ToString()))
                        {
                            //int secondUpperCellRow = row - 2;
                            if (generatedGroups[j].group.Contains(row.ToString() + (column - 2).ToString()))
                            {
                                generatedGroups.RemoveAt(i);
                                i--;
                                generatedGroups[j].group.Add(row.ToString() + column.ToString());
                                string firstCellData = generatedGroups[j].group[0];
                                int firstCellRow = int.Parse(firstCellData[0].ToString());
                                int firstCellColumn = int.Parse(firstCellData[1].ToString());

                                transform.GetChild(firstCellRow).GetChild(firstCellColumn).GetChild(0).GetComponent<Text>().text
                                    = generatedGroups[j].group.Count.ToString();

                                transform.GetChild(row).GetChild(column).GetChild(0).GetComponent<Text>().text = null;
                                isAdded = true;
                                //Debug.Log("Added To Right Group row " + row + " Column " + column);
                            }
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfCellIsOccupied(int row, int column)
    {
        string currentCellToCheck = row.ToString() + column.ToString();
        foreach(string a in occupiedCells)
        {
            if(a == currentCellToCheck)
            {
                return true;
            }
        }

        return false;
    }

    public int GetNumberOfEmptyCellsRight(int row, int column)
    {
        int numberOfEmptyCells = 1;

        for (; (column + 1) < maxColumns; column++)
        {
            if (CheckIfCellIsOccupied(row, column + 1) == false)
            {
                numberOfEmptyCells++;
            }
            else
            {
                break;
            }
        }

        //Debug.Log("Empty Cells on Right" + numberOfEmptyCells);
        return numberOfEmptyCells;
    }

    public int GetNumberOfEmptyCellsBelow(int row, int column)
    {
        int numberOfEmptyCells = 1;

        for(; (row + 1) < maxRows; row++)
        {
            if(CheckIfCellIsOccupied(row + 1, column) == false)
            {
                numberOfEmptyCells++;
            }
            else
            {
                break;
            }
        }

        //Debug.Log("Number of Empty Cells Below " + numberOfEmptyCells);
        return numberOfEmptyCells;
    }

    public void OccupieCell(int row, int column)
    {
        string data = row.ToString() + column.ToString();
        occupiedCells.Add(data);
    }
}

[System.Serializable]
public class Group
{
    public List<string> group = new List<string>();
}
