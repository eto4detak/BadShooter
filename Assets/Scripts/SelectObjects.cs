using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjects : MonoBehaviour
{
    public static List<Unit> allowedSelectObj; // массив всех юнитов, которых мы можем выделить
    public static List<Unit> selectedObjects; // выделенные объекты
    public static List<UnitGroup> selectedGroups; // выделенные объекты

    public GUISkin skin;
    private Rect rect;
    private bool draw;
    private Vector2 startPos;
    private Vector2 endPos;
    private float minY = 0;

    void Awake()
    {
        selectedObjects = new List<Unit>();
        selectedGroups = new List<UnitGroup>();
    }

    private void Start()
    {
        allowedSelectObj = GManager.pController.PlayerUnits;
    }

    private void Update()
    {
        if(selectedObjects != null)
        {
            foreach (var selected in selectedObjects)
            {
               // Debug.Log(selected.name + " command = " + selected.command);
            }
        }
    }


    void OnGUI()
    {
        TryDrawSelectBox();
    }

    private void TryDrawSelectBox()
    {
        GUI.skin = skin;
        GUI.depth = 99;
        if (Input.GetMouseButtonDown(0))
        {
           // Deselect();
            startPos = Input.mousePosition;
            draw = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            draw = false;
            HighlightSelected();
        }

        if (draw)
        {
            if (!CheckPosition(startPos, endPos)) return;
            endPos = Input.mousePosition;
            if (startPos == endPos) return;
            endPos = GetAvailablePosition(endPos);
            selectedObjects.Clear();
            selectedGroups.Clear();

            DrawSelectBox();
        }
    }

    private void DrawSelectBox()
    {
        rect = new Rect(Mathf.Min(endPos.x, startPos.x),
                        Screen.height - Mathf.Max(endPos.y, startPos.y),
                        Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
                        Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y)
                        );

        GUI.Box(rect, "");

        for (int j = 0; j < allowedSelectObj.Count; j++)
        {
            if (allowedSelectObj[j] == null) continue;
            // трансформируем позицию объекта из мирового пространства, в пространство экрана
            Vector2 tmp = new Vector2(Camera.main.WorldToScreenPoint(allowedSelectObj[j].transform.position).x,
                Screen.height - Camera.main.WorldToScreenPoint(allowedSelectObj[j].transform.position).y);

            if (rect.Contains(tmp)) // проверка, находится-ли текущий объект в рамке
            {
                if (allowedSelectObj[j].group == null)
                {
                    if (selectedObjects.Count == 0)
                    {
                        selectedObjects.Add(allowedSelectObj[j]);
                    }
                    else if (!CheckUnit(allowedSelectObj[j]))
                    {
                        selectedObjects.Add(allowedSelectObj[j]);
                    }
                }
                else
                {
                    if (!selectedGroups.Contains(allowedSelectObj[j].group))
                    {
                        selectedGroups.Add(allowedSelectObj[j].group);
                        //SelectGroup(allowedSelectObj[j].selfGroup);

                    }
                }
            }

        }
    }


    // проверка, добавлен объект или нет
    bool CheckUnit(Unit unit)
    {
        bool result = false;
        foreach (Unit u in selectedObjects)
        {
            if (u == unit) result = true;
        }
        return result;
    }


    public static bool HaveSelected()
    {
        if (selectedGroups.Count > 0 ) return true;
        return false;
    }


    public static void SetAllowed(Unit obj)
    {
        allowedSelectObj.Add(obj);
    }

    private static void HighlightSelected()
    {
        List<Unit> unitForHighlight = new List<Unit>();
        unitForHighlight.AddRange(selectedObjects);
        foreach (var group in selectedGroups)
        {
            unitForHighlight.AddRange(group.units);
        }
        HighlightManager.HighlightUnits(unitForHighlight);
    }

    public static void TrySelectUnit(Unit unit)
    {
        Unit findUnit = allowedSelectObj.Find(x => x.Equals(unit));
        if (findUnit != null)
        {
            selectedGroups.Add(unit.group);
            HighlightSelected();
        }
        

        //if (unit.selfGroup == null)
        //{
        //    Unit findUnit = allowedSelectObj.Find(x => x.Equals(unit));
        //    if (findUnit != null)
        //    {
        //        selectedObjects.Add(unit);
        //        HighlightSelected();
        //    }
        //}
        //else
        //{
        //    selectedGroups.Add(unit.selfGroup);
        //}
    }

    public static void SelectGroup(UnitGroup group)
    {
        foreach (var groupUnit in group.units)
        {
            selectedObjects.Add(groupUnit);
        }
    }

    //public static void DeselectUnit(Unit unit)
    //{
    //    selectedObjects.Remove(unit);
    //}

    public static void Deselect()
    {

        selectedObjects.Clear();
        selectedGroups.Clear();
        HighlightManager.Clear();
    }

    private Vector2 GetAvailablePosition(Vector2 point)
    {
        if (point.y < minY)
        {
            point.y = minY;
        }
        return point;
    }

    public void SetForbiddenPosition(Vector2 point)
    {
        minY = point.y;
    }

    //public void SetSelected(Rect rect)
    //{
    //    for (int j = 0; j < allowedSelectObj.Count; j++)
    //    {
    //        // трансформируем позицию объекта из мирового пространства, в пространство экрана
    //        //Vector2 tmp1 = new Vector2(Camera.main.WorldToScreenPoint(allowedSelectObj[j].transform.position).x,
    //        //    Screen.height - Camera.main.WorldToScreenPoint(allowedSelectObj[j].transform.position).y);

    //        Vector2 unitPoint = new Vector2(Camera.main.WorldToScreenPoint(allowedSelectObj[j].transform.position).x,
    //            Camera.main.WorldToScreenPoint(allowedSelectObj[j].transform.position).y);


    //        if (rect.Contains(unitPoint)) // проверка, находится-ли текущий объект в рамке
    //        {

    //            if(allowedSelectObj[j].selfGroup == null)
    //            {
    //                Debug.Log(" null allowedSelectObj[j].selfGroup");

    //                if (selectedObjects.Count == 0)
    //                {
    //                    selectedObjects.Add(allowedSelectObj[j]);
    //                }
    //                else if (!CheckUnit(allowedSelectObj[j]))
    //                {
    //                    selectedObjects.Add(allowedSelectObj[j]);
    //                }
    //            }
    //            else
    //            {

    //                Debug.Log("allowedSelectObj[j].selfGroup");

    //                foreach (var groupUnit in allowedSelectObj[j].selfGroup.units)
    //                {

    //                    Debug.Log("selected group");
    //                    selectedObjects.Add(groupUnit);
    //                }
    //            }


    //        }
    //    }
    //}

    private bool CheckPosition(Vector2 startPos, Vector2 endPos)
    {
        if(startPos.y > minY)
        {
            return true;
        }
        return false;
    }

    public static void ClearSelected()
    {
        selectedObjects.Clear();
        selectedGroups.Clear();
        HighlightManager.Clear();
    }

}