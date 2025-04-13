using ProdutosApp.Infra.Message.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.Message.Helpers
{
    /// <summary>
    /// Classe auxiliar para realizar o envio de e-mails.
    /// </summary>
    public class MailHelper
    {
        #region Atributos

        private readonly string _host = "localhost";
        private readonly int _port = 1025;
        private readonly string _from = "noreply@example.com";

        #endregion

        public void SendMail(ProdutoCriado produto)
        {
            //escrevendp p assunto do email
            var subject = "Produto cadastrado no sistema com sucesso - LojaProdutosApp";
            var userName = produto.Usuario == null ? "Usuário" : produto.Usuario;
            
            //escrevendo o corpo do email
            var body = $@"
                <h2>Produto cadastrado com sucesso!</h2>
                <h3>Olá, {userName}, o produto {produto.Nome} foi cadastrado com sucesso no sistema.</h3>
                <p>Dados do produto:</p>
                <ul>
                    <li>ID: {produto.Id}</li>
                    <li>Nome: {produto.Nome}</li>
                    <li>Preço: {produto.Preco}</li>
                    <li>Quantidade: {produto.Quantidade}</li>
                    <li>Criado em: {produto.CriadoEm:dd/MM/yyyy HH:mm:ss}</li>
                </ul>";

            //criando o objeto que fará o envio dos emails
            var smtpClient = new SmtpClient(_host, _port)
            {
                EnableSsl = false
            };

            //Configurando o remetente, destinatário, assunto e corpo da mensagem
            var mailMessage = new MailMessage(_from, "rafaelcaffonso2@gmail.com", subject, body);

            //Configurando o corpo da mensagem para HTML
            mailMessage.IsBodyHtml = true;

            smtpClient.Send(mailMessage);
        }
    }
}
