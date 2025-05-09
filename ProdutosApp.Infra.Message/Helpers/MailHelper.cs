﻿using ProdutosApp.Infra.Message.Models;
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
            var body = @$"
                <div style='font-family: Verdana, sans-serif; background-color: #f4f4f4; padding: 20px; text-align: center;'>
                    <div style='max-width: 600px; background-color: #ffffff; padding: 20px; border-radius: 8px; margin: auto; box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);'>
                        <img src='https://i.ibb.co/Q5NjJSS/logo.png'
                                alt='LojaProdutosApp' 
                                style='max-width: 200px; margin-bottom: 20px;' />
                        <h3 style='color: #333;'>Olá, {userName}, o produto {produto.Nome} foi cadastrado com sucesso no sistema.</h3>

                        <div style='background-color: #f0f0f0; padding: 15px; border-radius: 5px; margin: 20px 0;'>
                        <p>Dados do produto:</p>
                            <ul>
                                <li>ID: {produto.Id}</li>
                                <li>Nome: {produto.Nome}</li>
                                <li>Preço: {produto.Preco}</li>
                                <li>Quantidade: {produto.Quantidade}</li>
                                <li>Criado em: {produto.CriadoEm:dd/MM/yyyy HH:mm:ss}</li>
                            </ul>
                        </div>

                        <p style='font-size: 14px; color: #777;'>Se precisar de ajuda, entre em contato com nosso suporte.</p>

                        <hr style='border: none; border-top: 1px solid #ddd; margin: 20px 0;' />

                        <p style='font-size: 14px; font-weight: bold; color: #333;'>LojaProdutosApp</p>
                    </div>
                </div>";

            //criando o objeto que fará o envio dos emails
            var smtpClient = new SmtpClient(_host, _port)
            {
                EnableSsl = false
            };

            //Configurando o remetente, destinatário, assunto e corpo da mensagem
            var mailMessage = new MailMessage(_from, produto.Email, subject, body);

            //Configurando o corpo da mensagem para HTML
            mailMessage.IsBodyHtml = true;

            smtpClient.Send(mailMessage);
        }
    }
}
