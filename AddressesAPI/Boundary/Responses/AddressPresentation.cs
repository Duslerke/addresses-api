namespace AddressesAPI.Boundary.Responses
{
    public class AddressPresentation
    {
        public string LpiKey { get; set; }
        public string? LpiLogicalStatus { get; set; }
        public int LpiStartDate { get; set; }
        public int LpiEndDate { get; set; }
        public int LpiLastUpdateDate { get; set; }
        public int? Usrn { get; set; }
        public long Uprn { get; set; }
        public long? ParentUprn { get; set; }
        public int BlpuStartDate { get; set; }
        public int BlpuEndDate { get; set; }
        public string? BlpuClass { get; set; }
        public int BlpuLastUpdateDate { get; set; }
        public string? UsageDescription { get; set; }
        public string? UsagePrimary { get; set; }
        public bool PropertyShell { get; set; }
        public double Easting { get; set; }
        public double Northing { get; set; }
        public int? UnitNumber { get; set; }
        public string? SaoText { get; set; }
        public string? BuildingNumber { get; set; }
        public string? PaoText { get; set; }
        public int? PaonStartNum { get; set; }
        public string? StreetDescription { get; set; }
        public string? Locality { get; set; }
        public string? Ward { get; set; }
        public string? Town { get; set; }
        public string? Postcode { get; set; }
        public string? PostcodeNospace { get; set; }
        public string? PlanningUseClass { get; set; }
        public bool Neverexport { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? Gazetteer { get; set; }
        public string? Organisation { get; set; }
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        public string? Line4 { get; set; }
    }
}
