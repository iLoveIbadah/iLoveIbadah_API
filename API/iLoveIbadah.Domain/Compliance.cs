//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Compliance.Classification;
//using Microsoft.Extensions.Compliance.Redaction;
//using Microsoft.Extensions.Compliance.Classification.DataClassification;

//namespace iLoveIbadah.Domain
//{
//    public static class DataClassification
//    {
//        public static DataClassification EUIIDataClassification { get; } =
//            new DataClassification("EUIIDataTaxonomy", "EUIIData");

//        public static DataClassification EUPDataClassification { get; } =
//            new DataClassification("EUPDataTaxonomy", "EUPData");
//    }

//    public class EUIIDataAttribute : DataClassificationAttribute
//    {
//        public EUIIDataAttribute() : base(DataClassifications.EUIIDataClassification) { }
//    }

//    public class EUPDataAttribute : DataClassificationAttribute
//    {
//        public EUPDataAttribute() : base(DataClassifications.EUPDataClassification) { }
//    }
//}
