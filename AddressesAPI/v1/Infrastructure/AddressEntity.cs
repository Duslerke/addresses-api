using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressesAPI.v1.Infrastructure
{
    [Table("hackney_address")]
    public class AddressEntity
    {
        [Key]
        [StringLength(14)]
        [Column("lpi_key")]
        public string LpiKey { get; set; }
        
        [StringLength(18)]
        [Column("lpi_logical_status")]
        public string? LpiLogicalStatus { get; set; }
        
        [Column("lpi_start_date")]
        public int LpiStartDate { get; set; }
        
        [Column("lpi_end_date")]
        public int LpiEndDate { get; set; }
        
        [Column("lpi_last_update_date")]
        public int LpiLastUpdateDate { get; set; }
        
        [Column("usrn")]
        public int? Usrn { get; set; }
        
        [Column("uprn")]
        public long Uprn { get; set; }
        
        [Column("parent_uprn")]
        public long? ParentUprn { get; set; }
        
        [Column("blpu_start_date")]
        public int BlpuStartDate { get; set; }
        
        [Column("blpu_end_date")]
        public int BlpuEndDate { get; set; }
        
        [StringLength(4)]
        [Column("blpu_class")]
        public string? BlpuClass { get; set; }
        
        [Column("blpu_last_update_date")]
        public int BlpuLastUpdateDate { get; set; }
        
        [Column("usage_description")]
        public string? UsageDescription { get; set; }
        
        [Column("usage_primary")]
        public string? UsagePrimary { get; set; }
        
        [Column("property_shell")]
        public bool PropertyShell { get; set; }
        
        [Column("easting")]
        public double Easting { get; set; }
        
        [Column("northing")]
        public double Northing { get; set; }
        
        [Column("unit_number")]
        public int? UnitNumber { get; set; }
        
        [Column("sao_text")]
        public string? SaoText { get; set; }
        
        [StringLength(17)]
        [Column("building_number")]
        public string? BuildingNumber { get; set; }
        
        [Column("pao_text")]
        public string? PaoText { get; set; }
        
        [Column("paon_start_num")]
        public int? PaonStartNum { get; set; }
        
        [Column("street_description")]
        public string? StreetDescription { get; set; }
        
        [Column("locality")]
        public string? Locality { get; set; }
        
        [Column("ward")]
        public string? Ward { get; set; }
        
        [Column("town")]
        public string? Town { get; set; }
        
        [StringLength(8)]
        [Column("postcode")]
        public string? Postcode { get; set; }
        
        [StringLength(8)]
        [Column("postcode_nospace")]
        public string? PostcodeNospace { get; set; }
        
        [StringLength(8)]
        [Column("planning_use_class")]
        public string? PlanningUseClass { get; set; }
        
        [Column("neverexport")]
        public bool Neverexport { get; set; }
        
        [Column("longitude")]
        public double Longitude { get; set; }
        
        [Column("latitude")]
        public double Latitude { get; set; }
        
        [StringLength(8)]
        [Column("gazetteer")]
        public string? Gazetteer { get; set; }
        
        [Column("organisation")]
        public string? Organisation { get; set; }
        
        [Column("line1")]
        public string? Line1 { get; set; }
        
        [Column("line2")]
        public string? Line2 { get; set; }
        
        [Column("line3")]
        public string? Line3 { get; set; }
        
        [Column("line4")]
        public string? Line4 { get; set; }
    }
}
