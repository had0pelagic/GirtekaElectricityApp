namespace GirtekaElectricityApp.Models
{
    public class ElectricityModel
    {
        public string? Region { get; set; }//tinklas
        public string? ObjectName { get; set; }//obt_pavadinimas
        public string? ObjectType { get; set; }//obj_gv_tipas
        public string? ObjectNumber { get; set; }//obj_numeris
        public double? ElectricityConsumptionPerHour { get; set; }//p+
        public DateTime? Date { get; set; }//pl_t
        public double? GeneratedElectricityPerHour { get; set; }//p-
    }
}
