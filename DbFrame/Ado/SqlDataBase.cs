using System.Collections.Generic;
namespace DbFrame.Ado
{
    using System.Data;
    using System.Data.SqlClient;
    using DbFrame.Class;
    using Dapper;
    public class SqlDataBase
    {

        private string _ConnectionString { get; set; }

        public SqlDataBase()
        {
            this._ConnectionString = DbConfig.ConnectionString;
        }

        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(this._ConnectionString);
        }

        /// <summary>
        /// 分页 查询
        /// </summary>
        /// <returns></returns>
        public Paging FindPaging(string SqlStr, int Page, int PageSize, object _Param)
        {
            //解析参数
            var dic = new Dictionary<string, object>();
            if (_Param is Dictionary<string, object>)
            {
                dic = (Dictionary<string, object>)_Param;
            }
            else if (_Param is object)
            {
                dic = _Param.ToDictionary();
            }

            foreach (var item in dic)
            {
                SqlStr = SqlStr.Replace("@" + item.Key, item.Value == null ? null : "'" + item.Value + "' ");
            }

            var _DynamicParameters = new DynamicParameters();
            _DynamicParameters.Add("@SQL", SqlStr, DbType.String, ParameterDirection.Input);
            _DynamicParameters.Add("@PAGE", Page, DbType.Int32, ParameterDirection.Input);
            _DynamicParameters.Add("@PAGESIZE", PageSize, DbType.Int32, ParameterDirection.Input);
            _DynamicParameters.Add("@PAGECOUNT", 0, DbType.Int32, ParameterDirection.Output);
            _DynamicParameters.Add("@RECORDCOUNT", 0, DbType.Int32, ParameterDirection.Output);

            var _IDataReader = this.GetDbConnection().ExecuteReader("PROC_SPLITPAGE", _DynamicParameters, null, 30, CommandType.StoredProcedure);
            //将 IDataReader 对象转换为 DataSet 
            DataSet _DataSet = new DbFrame.Ado.DapperExtend.HZYDataSet();
            _DataSet.Load(_IDataReader, LoadOption.OverwriteChanges, null, new DataTable[] { });

            if (_DataSet.Tables.Count == 2)
            {
                return new Paging()
                 {
                     Table = _DataSet.Tables[1],
                     Total = _DynamicParameters.Get<int>("@RECORDCOUNT")
                 };
            }
            return new Paging();
        }


    }
}
