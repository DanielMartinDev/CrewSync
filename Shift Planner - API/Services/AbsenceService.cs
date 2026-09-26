using Shift_Planner___API.Data;
using Shift_Planner_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Shift_Planner_API.Services
{
    public class AbsenceService
    {
        private readonly ShiftPlannerContext shiftPlannerContext;

        public AbsenceService(ShiftPlannerContext context)
        {
            shiftPlannerContext = context;
        }

        public List<Absence> GetAbsences()
        {
            return shiftPlannerContext.Absences
                .Include(a => a.Employee)
                .AsNoTracking()
                .ToList();
        }

        public Absence? GetAbsence(int id)
        {
            return shiftPlannerContext.Absences
                .Include(a => a.Employee)
                .AsNoTracking()
                .FirstOrDefault(a => a.AbsenceID == id);
        }

        public Absence CreateAbsence(Absence absence)
        {
            if (absence.EndDate < absence.StartDate)
            {
                throw new Exception(
                    "Absence end date cannot be before the start date.");
            }

            var employeeExists =
                shiftPlannerContext.Employees
                    .Any(e => e.EmployeeID == absence.EmployeeID);

            if (!employeeExists)
            {
                throw new Exception(
                    "Employee could not be found.");
            }

            var overlappingAbsence =
                shiftPlannerContext.Absences
                    .Any(a =>
                        a.EmployeeID == absence.EmployeeID &&
                        absence.StartDate <= a.EndDate &&
                        absence.EndDate >= a.StartDate);

            if (overlappingAbsence)
            {
                throw new Exception(
                    "This employee already has an absence covering some or all of these dates.");
            }

            shiftPlannerContext.Absences.Add(absence);
            shiftPlannerContext.SaveChanges();

            return absence;
        }

        public bool UpdateAbsence(
        int id,
        Absence updatedAbsence)
        {
            var absence =
                shiftPlannerContext.Absences
                    .FirstOrDefault(
                        a => a.AbsenceID == id);

            if (absence == null)
                return false;

            if (updatedAbsence.EndDate <
                updatedAbsence.StartDate)
            {
                throw new Exception(
                    "Absence end date cannot be before the start date.");
            }

            var employeeExists =
                shiftPlannerContext.Employees
                    .Any(e =>
                        e.EmployeeID ==
                        updatedAbsence.EmployeeID);

            if (!employeeExists)
            {
                throw new Exception(
                    "Employee could not be found.");
            }

            var overlappingAbsence =
                shiftPlannerContext.Absences
                    .Any(a =>
                        a.AbsenceID != id &&
                        a.EmployeeID ==
                            updatedAbsence.EmployeeID &&
                        updatedAbsence.StartDate <= a.EndDate &&
                        updatedAbsence.EndDate >= a.StartDate);

            if (overlappingAbsence)
            {
                throw new Exception(
                    "This employee already has an absence covering some or all of these dates.");
            }

            absence.EmployeeID =
                updatedAbsence.EmployeeID;

            absence.StartDate =
                updatedAbsence.StartDate;

            absence.EndDate =
                updatedAbsence.EndDate;

            absence.Type =
                updatedAbsence.Type;

            absence.Notes =
                updatedAbsence.Notes;

            shiftPlannerContext.SaveChanges();

            return true;
        }

        public bool DeleteAbsence(int id)
        {
            var absence = shiftPlannerContext.Absences
                .FirstOrDefault(a => a.AbsenceID == id);

            if (absence == null)
                return false;

            shiftPlannerContext.Absences.Remove(absence);
            shiftPlannerContext.SaveChanges();

            return true;
        }
    }
}