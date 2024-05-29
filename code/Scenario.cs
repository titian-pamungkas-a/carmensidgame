using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario {
    private int id;
    private int scenarioId;
    private int order;
    private int cityId;

    public Scenario(int id, int scenarioId, int order, int cityId)
    {
        this.id = id;
        this.scenarioId = scenarioId;
        this.order = order;
        this.cityId = cityId;
    }

    public int Id { get => id; set => id = value; }
    public int ScenarioId { get => scenarioId; set => scenarioId = value; }
    public int Order { get => order; set => order = value; }
    public int CityId { get => cityId; set => cityId = value; }
}
