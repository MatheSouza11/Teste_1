using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Teste_1.Models;

using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;

namespace Teste_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(string usuario, string senha)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
            {
                ViewData["Message"] = "Usuário e senha são obrigatórios.";
                return View();
            }

            string connectionString = "Server=localhost;Database=cadastro;User=root;Password=123456;";
            string query = "INSERT INTO login (usuario, senha) VALUES (@usuario, @senha)";

            try
            {
                using var connection = new MySqlConnection(connectionString);
                using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@senha", senha);

                connection.Open();
                int linhasAfetadas = command.ExecuteNonQuery();

                ViewData["Message"] = linhasAfetadas > 0
                    ? "Usuário cadastrado com sucesso!"
                    : "Falha ao cadastrar usuário.";
            }
            catch (MySqlException ex)
            {
                ViewData["Message"] = $"Erro ao conectar ao banco de dados: {ex.Message}";
            }
            return View();

        }

        public IActionResult Cadastro()
        {
            return View();
        }


   
        public IActionResult Editar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Editar(int idLogin, string usuario, string senha)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
            {
                ViewData["Message"] = $"O nome de usuario e a senha não pode estar vazios";
                return View();
            }

            string connectionString = "Server=localhost;Database=cadastro;User=root;Password=123456;";
            string query = "UPDATE login SET usuario = @usuario, senha = @senha WHERE idLogin = @idLogin";

            try
            {
                using var connection = new MySqlConnection(connectionString);
                using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@senha", senha);
                command.Parameters.AddWithValue("@idLogin", idLogin);

                connection.Open();
                int linhasAfetadas = command.ExecuteNonQuery();


                ViewData["Message"] = linhasAfetadas > 0
                    ? "Usuario e senha foram alterados "
                    : "Falha ao alterar o usuário.";
            }
            catch(MySqlException ex)
            {
                ViewData["Message"] = $"Erro ao conectar ao banco de dados: {ex.Message}";
            }
            return View();
        }

        public IActionResult Excluir()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Excluir(int idLogin, string usuario, string senha)
        {
            if(string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
            {
                ViewData["Message"] = $"Os campos não podem estar vazios";
                return View();
            }

            string connectionString = "Server=localhost;Database=cadastro;User=root;Password=123456;";
            string query = "DELETE FROM login WHERE usuario = @usuario AND senha = @senha AND idLogin = @idLogin";

            try
            {
                using var connection = new MySqlConnection(connectionString);
                using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@senha", senha);
                command.Parameters.AddWithValue("@idLogin", idLogin);

                connection.Open();
                int linhasAfetadas = command.ExecuteNonQuery();

                ViewData["Message"] = linhasAfetadas > 0
                       ? "Usuario foi deletado "
                       : "Falha ao deletar o usuário.";
            }
            catch (MySqlException ex)
            {
                ViewData["Message"] = $"Erro ao conectar ao banco de dados: {ex.Message}";
            }

            return View();
        }



            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }      

