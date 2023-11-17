using AddressesAPI.Boundary.Responses;
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

        public static List<AddressDomain> EntityToDomain(this IList<AddressEntity> databaseEntities)
        {
            if (databaseEntities is null)
                return null;

            return databaseEntities.Select(dbAddress => dbAddress.EntityToDomain()).ToList();
        }

        public static AddressPresentation DomainToPresentation(this AddressDomain domainEntity)
        {
            if (domainEntity is null)
                return null;

            return new AddressPresentation
            {
                LpiKey = domainEntity.LpiKey,
                LpiLogicalStatus = domainEntity.LpiLogicalStatus,
                LpiStartDate = domainEntity.LpiStartDate,
                LpiEndDate = domainEntity.LpiEndDate,
                LpiLastUpdateDate = domainEntity.LpiLastUpdateDate,
                Usrn = domainEntity.Usrn,
                Uprn = domainEntity.Uprn,
                ParentUprn = domainEntity.ParentUprn,
                BlpuStartDate = domainEntity.BlpuStartDate,
                BlpuEndDate = domainEntity.BlpuEndDate,
                BlpuClass = domainEntity.BlpuClass,
                BlpuLastUpdateDate = domainEntity.BlpuLastUpdateDate,
                UsageDescription = domainEntity.UsageDescription,
                UsagePrimary = domainEntity.UsagePrimary,
                PropertyShell = domainEntity.PropertyShell,
                Easting = domainEntity.Easting,
                Northing = domainEntity.Northing,
                UnitNumber = domainEntity.UnitNumber,
                SaoText = domainEntity.SaoText,
                BuildingNumber = domainEntity.BuildingNumber,
                PaoText = domainEntity.PaoText,
                PaonStartNum = domainEntity.PaonStartNum,
                StreetDescription = domainEntity.StreetDescription,
                Locality = domainEntity.Locality,
                Ward = domainEntity.Ward,
                Town = domainEntity.Town,
                Postcode = domainEntity.Postcode,
                PostcodeNospace = domainEntity.PostcodeNospace,
                PlanningUseClass = domainEntity.PlanningUseClass,
                Neverexport = domainEntity.Neverexport,
                Longitude = domainEntity.Longitude,
                Latitude = domainEntity.Latitude,
                Gazetteer = domainEntity.Gazetteer,
                Organisation = domainEntity.Organisation,
                Line1 = domainEntity.Line1,
                Line2 = domainEntity.Line2,
                Line3 = domainEntity.Line3,
                Line4 = domainEntity.Line4,
            };
        }

        public static List<AddressPresentation> DomainToPresentation(this IList<AddressDomain> domainEntities)
        {
            if (domainEntities is null)
                return null;

            return domainEntities.Select(domainAddress => domainAddress.DomainToPresentation()).ToList();
        }
    }
}
