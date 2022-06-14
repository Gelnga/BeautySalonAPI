using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class WorkerService : BaseEntityService<App.BLL.DTO.Worker, App.DAL.DTO.Worker, IWorkerRepository>,
    IWorkerService
{
    public WorkerService(IWorkerRepository repository, IMapper<Worker, DAL.DTO.Worker> mapper) : base(repository,
        mapper)
    {
    }

    public async Task<ICollection<Worker>> GetWorkersBySalonIdAndServiceId(Guid salonId, Guid serviceId)
    {
        var workers = await Repository.GetWorkersWithSalonServices();
        return workers.Where(w => w.SalonWorkers!
                .Any(sw => sw.SalonServices!
                    .Any(ss => ss.SalonId == salonId && ss.ServiceId == serviceId)))
            .Select(w => MapWorker(w, Mapper, salonId, serviceId))
            .ToList();
    }

    private static Worker MapWorker(App.DAL.DTO.Worker worker, IMapper<Worker, DAL.DTO.Worker> mapper, Guid salonId,
        Guid serviceId)
    {
        var mapped = mapper.Map(worker)!;
        var salonServiceData = worker.SalonWorkers!
            .Select(e => e.SalonServices
                !.First(ss => ss.SalonId == salonId && ss.ServiceId == serviceId))
            .First(e => true);

        mapped.Price = salonServiceData.Price;
        mapped.UnitName = salonServiceData.Unit!.Name;
        mapped.ServiceDuration = salonServiceData.ServiceDuration;
        return mapped;
    }

    public async Task<List<Dictionary<string, TimeSpan>>> GetWorkerAvailableTimes(Guid id, DateOnly date, TimeSpan serviceDuration)
    {
        Console.WriteLine(date.ToString("dddd"));
        var worker = await Repository.GetWorkerWithAppointmentsAndSchedule(id);
        var workDay = worker.WorkSchedule!.WorkDays!
            .First(e => e.WeekDay.ToString() == date.ToString("dddd"));

        var workDayStartTimeSpan = workDay.WorkDayStart;
        var availableWorkDayTimes = new List<Dictionary<string, TimeSpan>>();

        while (workDayStartTimeSpan < workDay.WorkDayEnd)
        {
            var workDayEndTimeSpan = workDayStartTimeSpan.Add(serviceDuration);
            if (workDayEndTimeSpan > workDay.WorkDayEnd) break;
            if (!CheckTimePeriodsCrossing(workDayStartTimeSpan, workDayEndTimeSpan, 
                    workDay.LunchBreakStartTime!.Value, workDay.LunchBreakEndTime!.Value))
            {
                availableWorkDayTimes.Add(new Dictionary<string, TimeSpan>
                {
                    {"availableStartTime", workDayStartTimeSpan},
                    {"availableEndTime", workDayEndTimeSpan}
                });
            }

            workDayStartTimeSpan = workDayStartTimeSpan.Add(serviceDuration);
        }

        if (worker.Appointments == null || worker.Appointments.Count == 0)
        {
            return availableWorkDayTimes;
        }
        
        var dateAppointments = worker.Appointments!.Where(e => e.AppointmentDate == date);
        var appointmentsTimes = dateAppointments
            .Select(e => new Dictionary<string, TimeSpan>
            {
                {"appointmentStartTime", e.AppointmentStart},
                {"appointmentEndTime", e.AppointmentEnd}
            })
            .ToList();

        if (appointmentsTimes.Count == 0)
        {
            return availableWorkDayTimes;
        }

        var availableTimes = new List<Dictionary<string, TimeSpan>>();
        
        foreach (var availableWorkDayTime in availableWorkDayTimes)
        {
            var valid = true;
            var startingTime = availableWorkDayTime["availableStartTime"];
            var endingTime = availableWorkDayTime["availableEndTime"];
            
            foreach (var appointmentsTime in appointmentsTimes)
            {
                var appointmentStartTime = appointmentsTime["appointmentStartTime"];
                var appointmentEndTime = appointmentsTime["appointmentEndTime"];
                if (CheckTimePeriodsCrossing(startingTime, endingTime,
                        appointmentStartTime, appointmentEndTime))
                {
                    valid = false;
                }
            }
            
            if (!valid) continue;
            availableTimes.Add(new Dictionary<string, TimeSpan>
            {
                {"availableStartTime", startingTime},
                {"availableEndTime", endingTime}
            });
        }

        return availableTimes;
    }

    private bool CheckTimePeriodsCrossing(TimeSpan period1Start, TimeSpan period1End, TimeSpan period2Start, TimeSpan period2End)
    {
        if (period1Start >= period2Start && period1Start < period2End)
        {
            return true;
        }

        if (period1End > period2Start && period1End <= period2End)
        {
            return true;
        }

        if (period2Start > period1Start && period2Start < period1End &&
            period2End > period1Start && period2End < period1End)
        {
            return true;
        }

        return false;
    }

    public async Task<ICollection<Appointment>> GetWorkerAppointments(Guid workerId)
    {
        var worker = await Repository.GetWorkerWithAppointments(workerId);
        return Mapper.Map(worker)!.Appointments!;
    }
}