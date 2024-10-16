namespace Phenix.iTOS.CollaborativeTruckSchedulingService.DomainServices;

public interface ITrafficManagementService
{
    int DetermineSpeedingViolationInKmh(DateTime entryTimestamp, DateTime exitTimestamp);
    string GetRoadId();
}
