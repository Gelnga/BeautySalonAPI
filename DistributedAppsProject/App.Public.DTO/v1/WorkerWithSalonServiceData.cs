namespace App.Public.DTO.v1;

public class WorkerWithSalonServiceData : Worker
{
    public int Price { get; set; }
    
    public float ServiceDurationInHours { get; set; }

    public string UnitName { get; set; } = default!;
}