﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApp.Infra.Message.Settings
{
    public class RabbitMQSettings
    {
        public string Host => "localhost";
        
        public int Port => 5673;
        
        public string User => "guest";
        
        public string Password => "guest";

        public string Queue => "products_created";

        public string VirtualHost => "/";

    }
}
