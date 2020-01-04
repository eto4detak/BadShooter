using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjects : MonoBehaviour
{
    public WeaponPanel weaponPanel;
    public GUISkin skin;

    private Rect rect;
    private bool draw;
    private Vector2 startPos;
    private Vector2 endPos;
    private float minY = 0;
    private List<Unit> allowedSelectObj;
    private List<Unit> selected = new List<Unit>(); // выделенные объекты

    #region Singleton
    static protected SelectObjects s_Instance;
    static public SelectObjects instance { get { return s_Instance; } }

    public List<Unit> Selected { get => selected; }
    #endregion

    void Awake()
    {
        #region Singleton
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
        #endregion

        allowedSelectObj = Unit.allUnits;
        weaponPanel = (WeaponPanel)FindObjectOfType(typeof(WeaponPanel));
    }



    void OnGUI()
    {
        TryDrawSelectBox();
    }


    public void ClearSelected()
    {
        Selected.Clear();
        HighlightManager.Clear();
    }

    public bool HaveSelected()
    {
        if (Selected.Count > 0 ) return true;
        return false;
    }

    public void SelectUnit(Unit unit)
    {
        Selected.Add(unit);
        HighlightSelected();
        weaponPanel.SetTarget(unit.manager);
    }

    public void Deselect()
    {
        Selected.Clear();
        HighlightManager.Clear();
        weaponPanel.RemoveTarget();
    }

    public void SetForbiddenPosition(Vector2 point)
    {
        minY = point.y;
    }

    private bool CheckPosition(Vector2 startPos, Vector2 endPos)
    {
        if (startPos.y > minY)
        {
            return true;
        }
        return false;
    }
        private Vector2 GetAvailablePosition(Vector2 point)
    {
        if (point.y < minY)
        {
            point.y = minY;
        }
        return point;
    }
    private void HighlightSelected()
    {
        List<Unit> unitForHighlight = new List<Unit>();
        unitForHighlight.AddRange(Selected);
        HighlightManager.HighlightUnits(unitForHighlight);
    }

    // проверка, добавлен объект или нет
    bool CheckUnit(Unit unit)
    {
        bool result = false;
        foreach (Unit u in Selected)
        {
            if (u == unit) result = true;
        }
        return result;
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
                if (Selected.Count == 0)
                {
                    SelectUnit(allowedSelectObj[j]);
                }
                else if (!CheckUnit(allowedSelectObj[j]))
                {
                    SelectUnit(allowedSelectObj[j]);
                }
            }

        }
    }

    private void TryDrawSelectBox()
    {
        GUI.skin = skin;
        GUI.depth = 99;
        if (Input.GetMouseButtonDown(0))
        {
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
            Selected.Clear();

            DrawSelectBox();
        }
    }
}