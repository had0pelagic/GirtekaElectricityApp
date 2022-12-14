namespace GirtekaElectricityDomain
{
    public class FilteredElectricity
    {
        public Guid Id { get; set; }
        public string? Region { get; set; }//tinklas
        public string? ObjectName { get; set; }//obt_pavadinimas
        public string? ObjectType { get; set; }//obj_gv_tipas
        public long? ObjectNumber { get; set; }//obj_numeris
        public double? ElectricityConsumptionPerHour { get; set; }//p+
        public DateTime? Date { get; set; }//pl_t
        public double? GeneratedElectricityPerHour { get; set; }//p-
        public double? GeneratedAndConsumedDifference { get; set; }
    }
}
