using Microsoft.AspNetCore.Mvc;
using Shift_Planner___API.Data;
using Shift_Planner___API.Models;

namespace Shift_Planner___API.Services
{
    public class AvailabilityService
    {
        private readonly ShiftPlannerContext shiftPlannerContext;

        public AvailabilityService(ShiftPlannerContext context)
        {
            shiftPlannerContext = context;
        }

        public List<Availability> GetAvailabilities()
        {
            return shiftPlannerContext.Availabilities.ToList();
        }

        public Availability? GetAvailability(int id)
        {
            return shiftPlannerContext.Availabilities.FirstOrDefault(
                a => a.AvailabilityID == id);
        }

        public List<Availability> GetAvailabilityByEmployee(
        int employeeId)
        {
            return shiftPlannerContext.Availabilities
                .Where(a => a.EmployeeID == employeeId)
                .OrderBy(a => a.DayOfWeek)
                .ToList();
        }

        public Availability CreateAvailability(Availability availability)
        {
            shiftPlannerContext.Availabilities.Add(availability);
            shiftPlannerContext.SaveChanges();

            return availability;
        }

        public bool UpdateAvailability(int id, Availability updatedAvailability)
        {
            var availability = shiftPlannerContext.Availabilities.FirstOrDefault(a => a.AvailabilityID == id);

            if (availability == null)
                return false;

            availability.DayOfWeek = updatedAvailability.DayOfWeek;
            availability.AvailableFrom = updatedAvailability.AvailableFrom;
            availability.AvailableTo = updatedAvailability.AvailableTo;
            availability.IsAvailable = updatedAvailability.IsAvailable;

            shiftPlannerContext.SaveChanges();
            return true;
        }

        public bool DeleteAvailability(int id)
        {
            var availability = shiftPlannerContext.Availabilities.FirstOrDefault(a => a.AvailabilityID == id);

            if (availability == null)
                return false;

            shiftPlannerContext.Availabilities.Remove(availability);
            shiftPlannerContext.SaveChanges();
            return true;
        }
    }
}