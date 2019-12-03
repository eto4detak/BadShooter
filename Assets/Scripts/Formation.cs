using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    internal static float groupIndent = 10f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void MoveFormationRow(List<UnitGroup> selectedGroups, Vector3 newPosition)
    {
        if(selectedGroups.Count > 0)
        {
            Vector3[] groupPositions = Formation.GetSquareGroupPositions(new Vector3(), selectedGroups.Count, groupIndent);
            for (int i = 0; i < selectedGroups.Count; i++)
            {
                selectedGroups[i].MoveGroupToPoint2D(newPosition,  groupPositions[i]);
            }
        }
    }
    public static Vector3[] GetSquareGroupPositions(Vector3 newPosition, int unitCount, float distance)
    {
        int countInRow = Mathf.FloorToInt(Mathf.Sqrt(unitCount));
        Vector3[] newPositions = new Vector3[unitCount];
        float deltaX = countInRow / 2 * distance;
        float deltaZ = unitCount / countInRow / 2 * distance;
        for (int i = 0; i < unitCount; i++)
        {
            float x = i % countInRow * distance - deltaX;
            float z = Mathf.CeilToInt(i / countInRow) * distance - deltaZ;
            newPositions[i] = newPosition + new Vector3(x, 0, z);

        }
        return newPositions;
    }
}
