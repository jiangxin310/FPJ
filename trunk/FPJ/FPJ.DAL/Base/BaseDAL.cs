﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using DapperExtensions;
using System.Configuration;
using System.Data;
using FPJ.Model;

namespace FPJ.DAL.Base
{
    public class BaseDAL<T> where T : class
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        public BaseDAL(string ConnectionStringName = ConnectionStringNameConfig.DefaultDBName)
        {
            this.ConnectionString =ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        }

        #region insert/update/delete

        /// <summary>
        /// 实体新增
        /// </summary>
        /// <param name="entity">实体新增</param>
        /// <returns>返回dynamic类型，若主键为单，返回主键值，若主键为复合的，返回IDictionary<string,object></returns>
        public dynamic Insert(T entity)
        {
            if (entity == null)
            {
                return -1;
            }

            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.Insert<T>(entity);
            }
        }

        /// <summary>
        /// 实体更新（默认根据Id进行更新，所有字段更新操作）
        /// </summary>
        /// <param name="entity">更新实体</param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            if (entity == null)
            {
                return true;
            }

            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.Update<T>(entity);
            }
        }

        /// <summary>
        /// 实体删除（默认主键Id，如需其他主键名，实体模型中自定义ClassMapper
        /// </summary>
        /// <param name="entity">删除实体</param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            if (entity == null)
            {
                return true;
            }

            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.Delete<T>(entity);
            }
        }

        /// <summary>
        /// 删除实体（自定义删除条件）
        /// </summary>
        /// <param name="predicate">predicate表达式</param>
        /// <returns></returns>
        public bool Delete(object predicate)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.Delete<T>(predicate);
            }
        }

        #endregion

        #region get

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public T Get(int id)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.Get<T>(id);
            }
        }

        /// <summary>
        /// 获取单个实体（针对比较复杂的，可以自定义sql）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns></returns>
        public T Get(string sql, object param = null)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.Query<T>(sql, param).SingleOrDefault();
            }
        }

        /// <summary>
        /// 获取所有实体数量
        /// </summary>
        /// <param name="predicate">删选条件</param>
        /// <returns></returns>
        public int Count(object predicate = null)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.Count<T>(predicate);
            }
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns>返回受影响的行数</returns>
        public int Excute(string sql, object param = null, IDbTransaction transaction = null)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.Execute(sql, param, transaction);
            }
        }

        /// <summary>
        /// 执行sql，返回第一个值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns>返回 第一行第一列的元素</returns>
        public T ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.ExecuteScalar<T>(sql, param, transaction);
            }
        }

        /// <summary>
        /// 返回DataReader数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string sql, object param = null, IDbTransaction transaction = null)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.ExecuteReader(sql, param, transaction);
            }
        }

        /// <summary>
        /// 执行sql，返回多条执行结果
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Dapper.SqlMapper.GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.QueryMultiple(sql, param, transaction);
            }
        }

        #region List、PageList Operator

        /// <summary>
        /// 获取List集合（predicate表达式查询）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList(object predicate = null)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.GetList<T>(predicate);
            }
        }

        /// <summary>
        /// 自定义sql的List查询（大部分建议使用predicate表达式查询）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList(string sql, object param = null)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.Query<T>(sql, param);
            }
        }

        /// <summary>
        /// 两表联合查询
        /// </summary>
        /// <typeparam name="TFirst">第一个实体表</typeparam>
        /// <typeparam name="TSecond">第二个实体表</typeparam>
        /// <typeparam name="TReturn">返回实体</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="map">映射关系</param>
        /// <param name="param">sql参数</param>
        /// <returns>实体集合</returns>
        public IEnumerable<TReturn> GetList<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.Query(sql, map, param);
            }
        }

        /// <summary>
        /// 三表联合查询
        /// </summary>
        /// <typeparam name="TFirst">第一个实体表</typeparam>
        /// <typeparam name="TSecond">第二个实体表</typeparam>
        /// <typeparam name="TThird">第三个实体表</typeparam>
        /// <typeparam name="TReturn">返回实体</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="map">映射关系</param>
        /// <param name="param">sql参数</param>
        /// <returns>实体集合</returns>
        public IEnumerable<TReturn> GetList<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.Query(sql, map, param);
            }
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="page">页码</param>
        /// <param name="psize">页数</param>
        /// <param name="param">sql参数</param>
        /// <param name="order">分页排序依据</param>
        /// <param name="isnext">是否只查询有下一页数据，默认false（不包括分页总数、总数等相关参数）</param>
        /// <returns></returns>
        public Page<T> PageList(string sql, int page, int psize, object param = null, string order = " order by (select null)", bool isnext = false)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.PageList<T>(sql, page, psize, param, order, isnext);
            }
        }

        /// <summary>
        /// 两表分页查询
        /// </summary>
        /// <typeparam name="TFirst">第一个实体表</typeparam>
        /// <typeparam name="TSecond">第二个实体表</typeparam>
        /// <typeparam name="TReturn">返回实体</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="map">表映射关系</param>
        /// <param name="page">页码</param>
        /// <param name="psize">页数</param>
        /// <param name="param">sql参数</param>
        /// <param name="order">分页排序依据</param>
        /// <param name="splitOn">表之间分割字段（默认Id）</param>
        /// <param name="isnext">是否只查询有下一页数据，默认false（不包括分页总数、总数等相关参数）</param>
        /// <returns></returns>
        public Page<TReturn> PageList<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, int page, int psize,
            object param = null, string order = " order by (select null)", string splitOn = "Id", bool isnext = false)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.PageList(sql, map, page, psize, param, order, splitOn, isnext);
            }
        }

        /// <summary>
        /// 三表分页查询
        /// </summary>
        /// <typeparam name="TFirst">第一个实体表</typeparam>
        /// <typeparam name="TSecond">第二个实体表</typeparam>
        /// <typeparam name="TThird">第三个实体表</typeparam>
        /// <typeparam name="TReturn">返回实体</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="map">表映射关系</param>
        /// <param name="page">页码</param>
        /// <param name="psize">页数</param>
        /// <param name="param">sql参数</param>
        /// <param name="order">分页排序依据</param>
        /// <param name="splitOn">表之间分割字段（默认Id）</param>
        /// <param name="isnext">是否只查询有下一页数据，默认false（不包括分页总数、总数等相关参数）</param>
        /// <returns></returns>
        public Page<TReturn> PageList<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, int page, int psize,
           object param = null, string order = " order by (select null)", string splitOn = "Id", bool isnext = false)
        {
            using (var conn = Conn.GetConn(ConnectionString))
            {
                return conn.PageList(sql, map, page, psize, param, order, splitOn, isnext);
            }
        }

        #endregion

        #endregion
  
    }
}
