using FPJ.DAL;
using FPJ.DAL.Base;
using FPJ.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FPJ.BLL.Base
{
    /// <summary>
    /// 业务逻辑基类（其他BLL继承此类）
    /// 作用：共用常用的操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseBLL<T> where T : class
    {
        /// <summary>
        /// 数据库连接配置节点名
        /// </summary>
        protected string ConnectionStringName { get; set; }

        /// <summary>
        /// 基类数据访问DAL
        /// </summary>
        protected readonly BaseDAL<T> dal = null;

        public BaseBLL(string connectionStringName = ConnectionStringNameConfig.DefaultDBName)
        {
            this.ConnectionStringName = connectionStringName;
            dal = new BaseDAL<T>(this.ConnectionStringName);
        }

        #region Operator

        /// <summary>
        /// 实体新增
        /// </summary>
        /// <param name="entity">实体新增</param>
        /// <returns>返回dynamic类型，若主键为单，返回主键值，若主键为复合的，返回IDictionary<string,object></returns>
        public int Insert(T entity)
        {
            return dal.Insert(entity);
        }

        /// <summary>
        /// 实体更新（默认根据Id进行更新，所有字段更新操作）
        /// </summary>
        /// <param name="entity">更新实体</param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            return dal.Update(entity);
        }

        /// <summary>
        /// 实体删除（默认主键Id，如需其他主键名，实体模型中自定义ClassMapper
        /// </summary>
        /// <param name="entity">删除实体</param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            return dal.Delete(entity);
        }

        /// <summary>
        /// 删除实体（自定义删除条件）
        /// </summary>
        /// <param name="predicate">predicate表达式</param>
        /// <returns></returns>
        public bool Delete(object predicate)
        {
            return dal.Delete(predicate);
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public T Get(int id)
        {
            return dal.Get(id);
        }

        /// <summary>
        /// 获取单个实体（针对比较复杂的，可以自定义sql）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns></returns>
        public T Get(string sql, object param = null)
        {
            return dal.Get(sql, param);
        }

        /// <summary>
        /// 获取所有实体数量
        /// </summary>
        /// <param name="predicate">删选条件</param>
        /// <returns></returns>
        public int Count(object predicate = null)
        {
            return dal.Count(predicate);
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
            return dal.Excute(sql, param, transaction);
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
            return dal.ExecuteScalar(sql, param, transaction);
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
            return dal.ExecuteReader(sql, param, transaction);
        }

        #region List、PageList Operator

        /// <summary>
        /// 获取List集合（predicate表达式查询）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList(object predicate = null)
        {
            return dal.GetList(predicate);
        }

        /// <summary>
        /// 自定义sql的List查询（大部分建议使用predicate表达式查询）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList(string sql, object param = null)
        {
            return dal.GetList(sql, param);
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
            return dal.GetList(sql, map, param);
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
            return dal.GetList(sql, map, param);
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
            return dal.PageList(sql, page, psize, param, order, isnext);
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
            return dal.PageList(sql, map, page, psize, param, order, splitOn, isnext);
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
            return dal.PageList(sql, map, page, psize, param, order, splitOn, isnext);
        }

        #endregion
        #endregion
    }
}
