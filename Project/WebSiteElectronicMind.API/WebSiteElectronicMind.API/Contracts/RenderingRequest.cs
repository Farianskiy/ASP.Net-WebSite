using System.ComponentModel.DataAnnotations;

namespace WebSiteElectronicMind.API.Contracts
{
    public record RenderingRequest(
        [Required] InfoShield Shield,
        [Required] InfoElectrical Electrical,
        [Required] InfoCable Cable,
        [Required] string DegreeProtection,
        [Required] InfoOmentum Omentum,
        [Required] string PowerCable,
        [Required] InfoBuild Build,
        [Required] List<RenderingTable> RenderingTableList,
        string Comment = ""
        );

    public record InfoShield(
        [Required] string NameShield,
        [Required] string FullNameShield,
        [Required] string TypeShield
    );

    public record InfoElectrical(
        [Required] int NominalVoltage,
        [Required] int NominalShield,
        [Required] string TypeGrounding
    );

    public record InfoCable(
        [Required] string SupplyCable,
        [Required] string CableOL
    );

    public record InfoOmentum(
        [Required] string TypeOmentum,
        [Required] string QuantityOmentum,
        [Required] string TypeOmentumOL,
        [Required] string QuantityOmentumOL
    );

    public record InfoBuild(
        [Required] string FullNameEngineer,
        [Required] int NumberOrderCustomer,
        [Required] string NumberBuild
    );


    public record RenderingTable(
        [Required] string Name,
        [Required] string NameOfScheme,
        [Required] string Type,
        [Required] string NumberingLetter,
        [Required] string NumberBuild,
        int Polus,
        [Required] int Level,
        string NumberingDigit = "",
        string Phase = ""
        
    );
}
