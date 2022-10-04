using System.Collections.Generic;
using UnityEngine;

public class UnitsSpawner : SingletonMono<UnitsSpawner>
{
    [SerializeField] private GameField gameField;
    [SerializeField] private Transform unitsContainer;
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private int unitsCount;
    [Space]
    [SerializeField] private UnitDebugParametersGetter unitStartParamsGetter;

    private LinkedList<Unit> allUnits = new LinkedList<Unit>();
    private LinkedList<Unit> inactiveUnits = new LinkedList<Unit>();
    
    public LinkedList<Unit> AllUnits => allUnits;
    public LinkedList<Unit> InactiveUnits => inactiveUnits;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < unitsCount; i++)
            CreateUnit();
    }

    public void CreateUnit()
    {
        var unitObj = Instantiate(unitPrefab, unitsContainer);
        var unit = unitObj.GetComponent<Unit>();

        inactiveUnits.AddLast(unit);
        allUnits.AddLast(unit);

        unitObj.SetActive(false);
    }

    public void AddInactiveUnit(Unit unit)
    {
        this.inactiveUnits.AddLast(unit);
    }

    public Unit ReleaseLastInactive()
    {
        var unitNode = inactiveUnits.Last;
        var unit = unitNode.Value;

        Vector2 gameFieldLerpPosition = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        Vector3 spawnPosition = gameField.GetFieldPosition(gameFieldLerpPosition);

        unit.transform.position = spawnPosition;
        unit.Release(unitStartParamsGetter.Get());

        inactiveUnits.Remove(unitNode);

        return unit;
    }


    private void DeleteLast()
    {
        if (allUnits.Count == 0) return;

        var unit = allUnits.Last.Value;

        allUnits.RemoveLast();
        inactiveUnits.Remove(unit);
        
        unit.Destroy();
    }


    private void OnValidate()
    {
        if (Application.isPlaying && Time.realtimeSinceStartup > 3f)
        {
            int unitsCountDifference = unitsCount - allUnits.Count;
            
            if (unitsCountDifference > 0)
            {
                for (int i = 0; i < unitsCountDifference; i++)
                {
                    CreateUnit();
                }
            }
            else
            if (unitsCountDifference < 0)
            {
                for (int i = 0; i < Mathf.Abs(unitsCountDifference); i++)
                {
                    DeleteLast();
                }
            }
        }
    }
}
