using CureHospitalDALCrossPlatform;
using CureHospitalDALCrossPlatform.Models;

namespace CureHospitalTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            CureHospitalDbContext context = new CureHospitalDbContext();
            CureHospitalRepositoryEF repository = new CureHospitalRepositoryEF(context);

            int surgeryId;
            int result = repository.AddSurgeryDetails(1001, "ANE", DateTime.Today.AddDays(2), 15, 17, out surgeryId);
            Console.WriteLine("AddSurgeryDetails(1001, ANE) return=" + result + " surgeryId=" + surgeryId);

            int failedId;
            int failed = repository.AddSurgeryDetails(1005, "CAR", DateTime.Today.AddDays(2), 10, 12, out failedId);
            Console.WriteLine("AddSurgeryDetails(1005, CAR) return=" + failed + " surgeryId=" + failedId);
        }
    }
}
