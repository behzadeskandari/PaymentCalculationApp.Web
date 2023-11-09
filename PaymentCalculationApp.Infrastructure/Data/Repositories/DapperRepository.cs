using Microsoft.Data.SqlClient;
using PaymentCalculation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculationApp.Infrastructure.Data.Repositories
{
    public class DapperEmployeeRepository
    {
        //private readonly IDbConnection _dbConnection;
        //private string ConnectionString { get; }

        //public DapperEmployeeRepository(string connectionString)
        //{

        //    _dbConnection = new SqlConnection(connectionString);
        //    ConnectionString = connectionString;
        //}
        //public DapperEmployeeRepository(IDbConnection dbConnection)
        //{
        //    _dbConnection = dbConnection;
        //}

        //public IEnumerable<Employee> GetAllProducts()
        //{
        //    using (var connection = new SqlConnection(connString))
        //    {
        //        var sql = "SELECT * FROM Products";
        //        connection.Open();
        //        var employee = connection.Query<Employee>(sql).ToList();
        //    }
             
                
            
        //    return _dbConnection.Query<Product>();
        //}

        //public Product GetProductById(int productId)
        //{
        //    return _dbConnection.QueryFirstOrDefault<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = productId });
        //}

        //public void InsertProduct(Product product)
        //{
        //    var sql = "INSERT INTO Products (Name) VALUES (@Name)";
        //    _dbConnection.Execute(sql, product);
        //}

        //public void UpdateProduct(Product product)
        //{
        //    var sql = "UPDATE Products SET Name = @Name WHERE Id = @Id";
        //    _dbConnection.Execute(sql, product);
        //}

        //public void DeleteProduct(int productId)
        //{
        //    var sql = "DELETE FROM Products WHERE Id = @Id";
        //    _dbConnection.Execute(sql, new { Id = productId });
        //}

    }
}
