namespace App.Public.DTO.v1;

public class ServiceWithSalonServiceData : Service
{
    public int Price { get; set; }

    public string ServiceDuration { get; set; } = default!;

    public string UnitName { get; set; } = default!;
}