using MM.Base.Core.Models;
using System.Data;

namespace MM.Suppliers.API.Repositories
{
    public class SuppliersSqlQueryConstant
    {
        public const string GetSuppliersQuery = @"SELECT [SupplierId],[SupplierName],[ContactPerson],[ContactEmail],[Active]  FROM [dbo].[Suppliers] ";
        public const string GetSuppliersCountQuery = @"SELECT COUNT([SupplierId])  FROM [dbo].[Suppliers]";
        public const string InsertSqlQuery = @"INSERT INTO [dbo].[Suppliers]([SupplierName],[ContactPerson],[ContactEmail],[Active],[InsertedBy],[InsertDateTime]) 
                                             OUTPUT INSERTED.SupplierId, INSERTED.SupplierName, INSERTED.ContactPerson, INSERTED.ContactEmail,INSERTED.Active, INSERTED.InsertedBy, INSERTED.InsertDateTime                                             
                                             VALUES (@SupplierName, @ContactPerson, @ContactEmail, @Active, @InsertedBy, @InsertDateTime)";
        public const string UpdateSqlQuery = @"UPDATE [dbo].[Suppliers]   SET [SupplierName] = @SupplierName, [ContactPerson] = @ContactPerson ,[ContactEmail] = @ContactEmail
                                             ,[Active] = @Active ,[UpdatedBy] = @UpdatedBy ,[UpdateDateTime] = @UpdateDateTime WHERE [SupplierId] = @SupplierId";
        public const string DeleteSqlQuery = @"DELETE FROM [Suppliers] WHERE SupplierId = @Id";
      
        public const string GetAllSqlQuery = @"DECLARE @SQL nvarchar(1000)
                                               SET @SQL = 'SELECT [SupplierId],[SupplierName],[ContactPerson],[ContactEmail],[Active]  FROM [dbo].[Suppliers] '
                                               SET @SQL = @SQL + @Filter
                                               SET @SQL = @SQL + ' ORDER BY '+ @SortColumn +' '+ @SortBy ";

        public const string GetAllCountSqlQuery = @"DECLARE @SQL nvarchar(1000)
                                                  SET @SQL = 'SELECT COUNT([SupplierId])  FROM [dbo].[Suppliers]'
                                                  SET @SQL = @SQL + @Filter
                                                  EXEC (@SQL)";
        public static List<BasePagingSearchAllowedColumn> GetSuppliersAllowedColumnListForSearch()
        {
            var supplierAllowedColumns = new List<BasePagingSearchAllowedColumn> {                
            (new BasePagingSearchAllowedColumn() { ColumnName = "SupplierName", ColumnType = DbType.String }),
            (new BasePagingSearchAllowedColumn() { ColumnName = "ContactPerson", ColumnType = DbType.String }),
            (new BasePagingSearchAllowedColumn() { ColumnName = "ContactEmail", ColumnType = DbType.String }),
            (new BasePagingSearchAllowedColumn() { ColumnName = "Active", ColumnType = DbType.Boolean })
            };
            return supplierAllowedColumns;
        }
    }
}
