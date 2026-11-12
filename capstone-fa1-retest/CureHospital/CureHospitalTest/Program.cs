using System.Data;
using CureHospitalDALCrossPlatform;
using CureHospitalDALCrossPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace CureHospitalTest
{
    public class Program
    {
        static int passed;
        static int failed;

        static void Main(string[] args)
        {
            CureHospitalDbContext context = new CureHospitalDbContext();
            CureHospitalRepositoryEF repository = new CureHospitalRepositoryEF(context);

            DateTime pastDate = new DateTime(2025, 5, 9);
            DateTime bookedDay = DateTime.Today.AddDays(2);
            DateTime freeDay = DateTime.Today.AddDays(7);

            int surgeryId;
            int result = repository.AddSurgeryDetails(1003, "CAR", pastDate, 10, 12, out surgeryId);
            Check("published success date is now in the past, so the procedure returns -6", result == -6 && surgeryId == 0, result + " / " + surgeryId);

            result = repository.AddSurgeryDetails(1005, "CAR", pastDate, 10, 12, out surgeryId);
            Check("missing doctor returns -1", result == -1 && surgeryId == 0, result + " / " + surgeryId);

            result = repository.AddSurgeryDetails(1003, "ENT", pastDate, 10, 12, out surgeryId);
            Check("missing category returns -2", result == -2 && surgeryId == 0, result + " / " + surgeryId);

            result = repository.AddSurgeryDetails(1001, "CAR", pastDate, 10, 12, out surgeryId);
            Check("doctor specialization mismatch returns -3", result == -3 && surgeryId == 0, result + " / " + surgeryId);

            result = repository.AddSurgeryDetails(1003, "CAR", pastDate, 12, 10, out surgeryId);
            Check("start time not less than end time returns -4", result == -4 && surgeryId == 0, result + " / " + surgeryId);

            result = repository.AddSurgeryDetails(1003, null, pastDate, 10, 12, out surgeryId);
            Check("null category is a DAL exception and returns -98", result == -98 && surgeryId == 0, result + " / " + surgeryId);

            result = repository.AddSurgeryDetails(1001, "ANE", bookedDay, 10, 12, out surgeryId);
            Check("overlapping booking returns -5", result == -5 && surgeryId == 0, result + " / " + surgeryId);

            result = repository.AddSurgeryDetails(1003, "CAR", freeDay, 10, 12, out surgeryId);
            Check("future booking returns 1 and the next surgery id 5002", result == 1 && surgeryId == 5002, result + " / " + surgeryId);

            var details = repository.GetSurgeryDetails(freeDay);
            Check("table function returns the inserted surgery", details != null && details.Count == 1 && details[0].DoctorName.Trim() == "Susan", details == null ? "null" : details.Count.ToString());

            int count = ScalarCount(context, "Cardiologist", freeDay);
            Check("scalar function counts one cardiologist surgery", count == 1, count.ToString());

            bool? removed = repository.RemoveSurgeryDetails(freeDay);
            Check("remove by date deletes the inserted row", removed == true, removed.ToString());
            removed = repository.RemoveSurgeryDetails(freeDay);
            Check("remove by date returns false when nothing matches", removed == false, removed.ToString());

            int updated = repository.UpdateSurgeryTime(5000, 11, 15);
            Check("update surgery 5000 to 11-15 returns 1", updated == 1, updated.ToString());
            updated = repository.UpdateSurgeryTime(5000, 15, 11);
            Check("end time not greater than start time returns -1", updated == -1, updated.ToString());
            updated = repository.UpdateSurgeryTime(5007, 11, 15);
            Check("missing surgery 5007 returns -98", updated == -98, updated.ToString());
            repository.UpdateSurgeryTime(5000, 9, 14);

            var cardiologists = repository.FetchDoctorIDs("CAR");
            Check("CAR doctors are 1002 and 1003", cardiologists != null && cardiologists.Contains(1002) && cardiologists.Contains(1003), string.Join(",", cardiologists ?? new List<int>()));

            bool? added = repository.AddDoctorSpecialization(1001, "GYN", DateTime.Today);
            Check("Albert can be given GYN", added == true, added.ToString());
            added = repository.AddDoctorSpecialization(1001, "GYN", DateTime.Today);
            Check("duplicate specialization returns false", added == false, added.ToString());

            int doctorResult = AddDoctor(context, "Neil", "ENT", "ENT Specialist");
            Check("usp_AddDoctorDetails inserts a new doctor and a missing specialization", doctorResult == 1, doctorResult.ToString());

            Console.WriteLine();
            Console.WriteLine(passed + " passed, " + failed + " failed");
        }

        static void Check(string name, bool ok, string actual)
        {
            if (ok)
            {
                passed++;
                Console.WriteLine("PASS  " + name);
            }
            else
            {
                failed++;
                Console.WriteLine("FAIL  " + name + "  actual=" + actual);
            }
        }

        static int ScalarCount(CureHospitalDbContext context, string specializationName, DateTime surgeryDate)
        {
            var connection = context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT dbo.ufn_FetchSurgeryCount(@name, @date)";
            var name = command.CreateParameter();
            name.ParameterName = "@name";
            name.Value = specializationName;
            command.Parameters.Add(name);
            var date = command.CreateParameter();
            date.ParameterName = "@date";
            date.Value = surgeryDate.Date;
            command.Parameters.Add(date);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        static int AddDoctor(CureHospitalDbContext context, string doctorName, string code, string specializationName)
        {
            var connection = context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            using var command = connection.CreateCommand();
            command.CommandText = "DECLARE @r INT; EXEC @r = usp_AddDoctorDetails @DoctorName, @SpecializationCode, @SpecializationName; SELECT @r;";
            var name = command.CreateParameter();
            name.ParameterName = "@DoctorName";
            name.Value = doctorName;
            command.Parameters.Add(name);
            var specializationCode = command.CreateParameter();
            specializationCode.ParameterName = "@SpecializationCode";
            specializationCode.Value = code;
            command.Parameters.Add(specializationCode);
            var specialization = command.CreateParameter();
            specialization.ParameterName = "@SpecializationName";
            specialization.Value = specializationName;
            command.Parameters.Add(specialization);
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}
