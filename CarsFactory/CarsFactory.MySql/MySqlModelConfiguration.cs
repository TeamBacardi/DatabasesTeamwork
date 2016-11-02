using CarsFactory.MySql.Models;
using System.Collections.Generic;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace CarsFactory.MySql
{
    public class MySqlModelConfiguration : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> configurations = new List<MappingConfiguration>();

            var shopReports = new MappingConfiguration<ShopReport>();

            shopReports.HasProperty(c => c.Id).IsIdentity(KeyGenerator.Autoinc);

            shopReports.MapType(report => new
            {
                // Id = report.Id,
                ShopName = report.ShopName,
                
                TurnOver = report.TurnOver
                
            }).ToTable("shop-reports");

            configurations.Add(shopReports);

            return configurations;
        }
    }
}
