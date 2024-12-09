namespace WebSiteElectronicMind.MVC.Models.RenderingToPDF
{
    public class TableFull
    {
        public class RenderingRequest {
            public InfoShield? Shield { get; set; }
            public InfoElectrical? Electrical { get; set; }
            public InfoCable? Cable { get; set; }
            public string? DegreeProtection { get; set; }
            public InfoOmentum? Omentum { get; set; }
            public string? PowerCable { get; set; }
            public string? Comment { get; set; }
            public InfoBuild? Build { get; set; }
            public List<RenderingTable>? RenderingTableList { get; set; }
        };

        public class InfoShield {
            public string? NameShield { get; set; }
            public string? FullNameShield { get; set; }
            public string? TypeShield { get; set; }
        };

        public class InfoElectrical {
            public int NominalVoltage { get; set; }
            public int NominalShield { get; set; }
            public string? TypeGrounding { get; set; }
        };

        public class InfoCable {
            public string? SupplyCable { get; set; }
            public string? CableOL { get; set; }
        };

        public class InfoOmentum {
            public string? TypeOmentum { get; set; }
            public string? QuantityOmentum { get; set; }
            public string? TypeOmentumOL { get; set; }
            public string? QuantityOmentumOL { get; set; }
        };

        public class InfoBuild {
            public string? FullNameEngineer { get; set; }
            public int NumberOrderCustomer { get; set; }
            public string? NumberBuild { get; set; }
        };


        public class RenderingTable {
            public string? Name { get; set; }
            public string? NameOfScheme { get; set; }
            public string? Type { get; set; }
            public string? NumberingLetter { get; set; }
            public string? NumberingDigit { get; set; }
            public string? Phase { get; set; }
            public int Polus { get; set; }
            public int Level { get; set; }
            public string? NumberBuild { get; set; }
        };
    }
}
