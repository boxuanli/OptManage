using System;
namespace DbFrame.Class
{
    using System.Data;
    public class DbConfig
    {

        public static string ConnectionString { get; set; }

        public static EDataBaseType _EDataBaseType = EDataBaseType.SqlServer;

        public static Func<string, IDbConnection> GetDbConnection;

        public static Func<string, int, int, object, Paging> FindPaging;

        public static string GetLastInsertId = string.Empty;

        public static Func<string, string, string, object, string> FindMaxNumber;


    }
}
