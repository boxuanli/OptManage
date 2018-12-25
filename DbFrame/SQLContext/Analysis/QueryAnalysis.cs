using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.SqlContext
{
    using System.Linq.Expressions;
    using Abstract;
    using Interface;
    using Class;



    public class QueryAnalysis<T1> : AbstractQuery<T1>
       where T1 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1> Select(Expression<Func<T1, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1> Where(Expression<Func<T1, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1> WhereIF(bool IsWhere, Expression<Func<T1, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1> GroupBy(Expression<Func<T1, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1> OrderBy(Expression<Func<T1, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }









    }




    public class QueryAnalysis<T1, T2> : AbstractQuery<T1, T2>
        where T1 : class,new()
        where T2 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2> Select(Expression<Func<T1, T2, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2> Where(Expression<Func<T1, T2, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2> WhereIF(bool IsWhere, Expression<Func<T1, T2, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2> GroupBy(Expression<Func<T1, T2, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2> OrderBy(Expression<Func<T1, T2, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2> Join(Expression<Func<T1, T2, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3> : AbstractQuery<T1, T2, T3>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3> Select(Expression<Func<T1, T2, T3, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3> GroupBy(Expression<Func<T1, T2, T3, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3> OrderBy(Expression<Func<T1, T2, T3, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3> Join(Expression<Func<T1, T2, T3, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3, T4> : AbstractQuery<T1, T2, T3, T4>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3, T4> Select(Expression<Func<T1, T2, T3, T4, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3, T4> Where(Expression<Func<T1, T2, T3, T4, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4> GroupBy(Expression<Func<T1, T2, T3, T4, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4> OrderBy(Expression<Func<T1, T2, T3, T4, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3, T4> Join(Expression<Func<T1, T2, T3, T4, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3, T4, T5> : AbstractQuery<T1, T2, T3, T4, T5>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3, T4, T5> Select(Expression<Func<T1, T2, T3, T4, T5, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5> GroupBy(Expression<Func<T1, T2, T3, T4, T5, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5> OrderBy(Expression<Func<T1, T2, T3, T4, T5, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3, T4, T5> Join(Expression<Func<T1, T2, T3, T4, T5, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3, T4, T5, T6> : AbstractQuery<T1, T2, T3, T4, T5, T6>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3, T4, T5, T6> Select(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6> Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3, T4, T5, T6> Join(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3, T4, T5, T6, T7> : AbstractQuery<T1, T2, T3, T4, T5, T6, T7>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3, T4, T5, T6, T7> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8> : AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9> : AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
        where T10 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
        where T10 : class,new()
        where T11 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
        where T10 : class,new()
        where T11 : class,new()
        where T12 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
        where T10 : class,new()
        where T11 : class,new()
        where T12 : class,new()
        where T13 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
        where T10 : class,new()
        where T11 : class,new()
        where T12 : class,new()
        where T13 : class,new()
        where T14 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }




    public class QueryAnalysis<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : AbstractQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
        where T1 : class,new()
        where T2 : class,new()
        where T3 : class,new()
        where T4 : class,new()
        where T5 : class,new()
        where T6 : class,new()
        where T7 : class,new()
        where T8 : class,new()
        where T9 : class,new()
        where T10 : class,new()
        where T11 : class,new()
        where T12 : class,new()
        where T13 : class,new()
        where T14 : class,new()
        where T15 : class,new()
    {
        public QueryAnalysis()
        { }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Select(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> Column)
        {
            this.CodeSelect(Column);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> Where)
        {
            this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> WhereIF(bool IsWhere, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> Where)
        {
            if (IsWhere) this.CodeWhere(Where);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> GroupBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> GroupBy)
        {
            this.CodeGroupBy(GroupBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> OrderBy(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object>> OrderBy)
        {
            this.CodeOrderBy(OrderBy);
            return this;
        }

        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> SqlString(string Sql, object Param)
        {
            this.CodeSqlString(Sql, Param);
            return this;
        }




        public override IQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Join(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, bool>> ON, string JoinTabName, EJoinType _EJoinType)
        {
            this.CodeJoin(_EJoinType, ON, JoinTabName);
            return this;
        }





    }













}
