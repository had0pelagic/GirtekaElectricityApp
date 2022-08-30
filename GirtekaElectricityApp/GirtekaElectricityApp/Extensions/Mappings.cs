using GirtekaElectricityApp.Models;
using GirtekaElectricityDomain;

namespace GirtekaElectricityApp.Extensions
{
    public static class Mappings
    {
        /// <summary>
        /// Maps FilteredElectricity to ElectricityModel
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
        /// Maps Electricity to FilteredElectricity
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
                GeneratedAndConsumedDifference = GetGeneratedAndConsumedDifference(item.ElectricityConsumptionPerHour, item.GeneratedElectricityPerHour)
            };
        }

        /// <summary>
        /// Maps FilteredElectricity to FilteredElectricityModel
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static FilteredElectricityModel ToFilteredElectricityModel(this FilteredElectricity item)
        {
            return new FilteredElectricityModel
            {
                Region = item.Region,
                ObjectName = item.ObjectName,
                ObjectType = item.ObjectType,
                ObjectNumber = item.ObjectNumber,
                ElectricityConsumptionPerHour = item.ElectricityConsumptionPerHour,
                Date = item.Date,
                GeneratedElectricityPerHour = item.GeneratedElectricityPerHour,
                GeneratedAndConsumedDifference = item.GeneratedAndConsumedDifference
            };
        }

        /// <summary>
        /// Maps ElectricityModel to Electricity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns difference between generated and consumed data
        /// </summary>
        /// <param name="generated"></param>
        /// <param name="consumed"></param>
        /// <returns></returns>
        private static double? GetGeneratedAndConsumedDifference(double? consumed, double? generated)
        {
            if (consumed == null || generated == null)
            {
                return null;
            }

            return Math.Abs(Convert.ToDouble(consumed - generated));
        }
    }
}
