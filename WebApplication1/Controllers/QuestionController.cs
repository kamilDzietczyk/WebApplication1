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
    public class QuestionController : Controller
    {

        private readonly IConfiguration _configuration;

        public QuestionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "SELECT * FROM quizapi.question";
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
            string query = "SELECT * FROM quizapi.question where questionId=@DepartmentId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QuizAppCon");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@DepartmentId",id);
                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet("quizId/{id}")]
        public JsonResult GetByQuizId(int id)
        {
            string query = "SELECT * FROM quizapi.question where quizId=@quizId";
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

        [HttpPost]
        public JsonResult Post(Question que)
        {
            string query = "INSERT INTO quizapi.question(questionText, answerOne, answerTwo, answerThree, correctAnswer, answerPoint, quizId)VALUES (@questionText,@answerOne,@answerTwo,@answerThree,@correctAnswer,@answerPoint,@quizId)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QuizAppCon");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@questionText", que.questionText);
                    mySqlCommand.Parameters.AddWithValue("@answerOne", que.answerTwo);
                    mySqlCommand.Parameters.AddWithValue("@answerTwo", que.answerTwo);
                    mySqlCommand.Parameters.AddWithValue("@answerThree", que.answerThree);
                    mySqlCommand.Parameters.AddWithValue("@correctAnswer", que.correctAnswer);
                    mySqlCommand.Parameters.AddWithValue("@answerPoint", que.answerPoint);
                    mySqlCommand.Parameters.AddWithValue("@quizId", que.quizId);

                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Question added successfully");
        }

        [HttpPut("{id}")]
        public JsonResult Put(Question que, int id)
        {
            string query = "UPDATE quizapi.question SET questionText=@questionText, answerOne=@answerOne, answerTwo=@answerTwo, answerThree=@answerThree, correctAnswer=@correctAnswer, answerPoint=@answerPoint, quizId=@quizId WHERE (questionId = @ID);";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QuizAppCon");
            MySqlDataReader myreader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, mycon))
                {
                    mySqlCommand.Parameters.AddWithValue("@ID", id);
                    mySqlCommand.Parameters.AddWithValue("@questionText", que.questionText);
                    mySqlCommand.Parameters.AddWithValue("@answerOne", que.answerTwo);
                    mySqlCommand.Parameters.AddWithValue("@answerTwo", que.answerTwo);
                    mySqlCommand.Parameters.AddWithValue("@answerThree", que.answerThree);
                    mySqlCommand.Parameters.AddWithValue("@correctAnswer", que.correctAnswer);
                    mySqlCommand.Parameters.AddWithValue("@answerPoint", que.answerPoint);
                    mySqlCommand.Parameters.AddWithValue("@quizId", que.quizId);

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
            string query = "DELETE FROM quizapi.question WHERE (questionId = @ID)";
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
