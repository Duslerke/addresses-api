using AddressesAPI.v1.Domain;
using AddressesAPI.v1.Infrastructure;

namespace AddressesAPI.v1.Factories
{
    public static class AddressFactory
    {
        public static AddressDomain EntityToDomain(this AddressEntity databaseEntity)
        {
            if (databaseEntity is null)
                return null;

            return new AddressDomain
            {
                LpiKey = databaseEntity.LpiKey,
                LpiLogicalStatus = databaseEntity.LpiLogicalStatus,
                LpiStartDate = databaseEntity.LpiStartDate,
                LpiEndDate = databaseEntity.LpiEndDate,
                LpiLastUpdateDate = databaseEntity.LpiLastUpdateDate,
                Usrn = databaseEntity.Usrn,
                Uprn = databaseEntity.Uprn,
                ParentUprn = databaseEntity.ParentUprn,
                BlpuStartDate = databaseEntity.BlpuStartDate,
                BlpuEndDate = databaseEntity.BlpuEndDate,
                BlpuClass = databaseEntity.BlpuClass,
                BlpuLastUpdateDate = databaseEntity.BlpuLastUpdateDate,
                UsageDescription = databaseEntity.UsageDescription,
                UsagePrimary = databaseEntity.UsagePrimary,
                PropertyShell = databaseEntity.PropertyShell,
                Easting = databaseEntity.Easting,
                Northing = databaseEntity.Northing,
                UnitNumber = databaseEntity.UnitNumber,
                SaoText = databaseEntity.SaoText,
                BuildingNumber = databaseEntity.BuildingNumber,
                PaoText = databaseEntity.PaoText,
                PaonStartNum = databaseEntity.PaonStartNum,
                StreetDescription = databaseEntity.StreetDescription,
                Locality = databaseEntity.Locality,
                Ward = databaseEntity.Ward,
                Town = databaseEntity.Town,
                Postcode = databaseEntity.Postcode,
                PostcodeNospace = databaseEntity.PostcodeNospace,
                PlanningUseClass = databaseEntity.PlanningUseClass,
                Neverexport = databaseEntity.Neverexport,
                Longitude = databaseEntity.Longitude,
                Latitude = databaseEntity.Latitude,
                Gazetteer = databaseEntity.Gazetteer,
                Organisation = databaseEntity.Organisation,
                Line1 = databaseEntity.Line1,
                Line2 = databaseEntity.Line2,
                Line3 = databaseEntity.Line3,
                Line4 = databaseEntity.Line4,
            };
        }

        public static List<AddressDomain> EntityToDomain(this IList<AddressEntity> databaseEnties)
        {
            if (databaseEnties is null)
                return null;

            return databaseEnties.Select(dbAddress => dbAddress.EntityToDomain()).ToList();
        }
    }
}
