using GirtekaElectricityApp.Models;
using GirtekaElectricityDomain;

namespace GirtekaElectricityApp.Extensions
{
    public static class Mappings
    {
        /// <summary>
        /// Maps filtered electricity to electricity model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ElectricityModel ToElectricityModel(this FilteredElectricity item)
        {
            return new ElectricityModel
            {
                Region = item.Region,
                ObjectName = item.ObjectName,
                ObjectType = item.ObjectType,
                ObjectNumber = item.ObjectNumber,
                ElectricityConsumptionPerHour = item.ElectricityConsumptionPerHour,
                Date = item.Date,
                GeneratedElectricityPerHour = item.GeneratedElectricityPerHour,
            };
        }

        /// <summary>
        /// Maps electricity to filtered electricity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static FilteredElectricity ToFilteredElectricity(this Electricity item)
        {
            return new FilteredElectricity
            {
                Region = item.Region,
                ObjectName = item.ObjectName,
                ObjectType = item.ObjectType,
                ObjectNumber = item.ObjectNumber,
                ElectricityConsumptionPerHour = item.ElectricityConsumptionPerHour,
                Date = item.Date,
                GeneratedElectricityPerHour = item.GeneratedElectricityPerHour,
            };
        }

        public static Electricity ToElectricity(this ElectricityModel item)
        {
            return new Electricity
            {
                Region = item.Region,
                ObjectName = item.ObjectName,
                ObjectType = item.ObjectType,
                ObjectNumber = item.ObjectNumber,
                ElectricityConsumptionPerHour = item.ElectricityConsumptionPerHour,
                Date = item.Date,
                GeneratedElectricityPerHour = item.GeneratedElectricityPerHour,
            };
        }
    }
}
