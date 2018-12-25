namespace DbFrame.Ado.DapperExtend
{
    using System.Data;
    using System.Data.Common;
    public class HZYDataAdapter : DataAdapter
    {
        public HZYDataAdapter() { }

        public int FillFromReader(DataSet _DataSet, IDataReader _IDataReader, int StartRecord, int MaxRecords)
        {
            var TableName = _IDataReader.GetSchemaTable().Rows[0]["BaseTableName"].ToString();
            return this.Fill(_DataSet, TableName, _IDataReader, StartRecord, MaxRecords);
        }


    }
}
