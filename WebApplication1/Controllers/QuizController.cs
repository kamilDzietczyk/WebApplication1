using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class QuizController : Controller
    {
        private readonly IConfiguration _configuration;

        public QuizController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "SELECT * FROM quizapi.quiz";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QuizAppCon");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet("nameId/{id}")]
        public JsonResult GetById(int id)
        {
            string query = "SELECT * FROM quizapi.quiz where quizId=@quizId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QuizAppCon");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@quizId", id);
                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet("Sort")]
        public JsonResult GetSort()
        {
            string query = "SELECT Max(quizId) FROM quizapi.quiz";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QuizAppCon");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult PostQuiz(Quiz qu) {
            string query = "INSERT INTO quizapi.quiz (quizName) VALUES(@quizName);";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QuizAppCon");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@quizName", qu.quizName); ;

                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Question post successfully");
        }


        [HttpPut("{id}")]
        public JsonResult Put(Quiz qu, int id)
        {
            string query = "UPDATE quizapi.quiz SET quizName=@quizName WHERE (quizId = @ID);";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QuizAppCon");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@ID", id);
                    mySqlCommand.Parameters.AddWithValue("@quizName", qu.quizName);;

                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Question updated successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = "DELETE FROM quizapi.quiz WHERE (quizId = @ID)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QuizAppCon");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@ID", id);

                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Question delete successfully");
        }
    }
}
