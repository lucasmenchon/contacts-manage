Este é um app web feito com aspnet core para exemplo, e como consegui realizar deploy dele na Oracle Cloud com Nginx.

Fonte: https://labs.sogeti.com/deploy-net-core-web-api-to-linux-ubuntu/
 
## **Primeiro foi instalado o DOTNET NO UBUNTU.**

<pre><code>sudo apt-get install wget apt-transport-https</code></pre>
<pre><code>sudo wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb</code></pre>
<pre><code>sudo dpkg -i packages-microsoft-prod.deb</code></pre>
<pre><code>sudo apt-get update</code></pre>
<pre><code>sudo apt-get install dotnet-sdk-6.0</code></pre>

**Configuração do Linux VM**

Você precisará de acesso SSH à máquina virtual e permissões de raiz. Em primeiro lugar, vamos atualizar o VM e instalar o software mínimo necessário. 

<pre><code>sudo apt update</code></pre>
<pre><code>sudo apt -y upgrade</code></pre>



**upgrade** é usado para instalar as versões mais recentes de todos os pacotes atualmente instalados no sistema 

sudo reboot

Após a execução do comando de atualização, precisamos reiniciar um VM para garantir que a atualização seja concluída. 
## **Baixe e instale o Runtime** 
Para executar um aplicativo .Net Core em qualquer sistema operacional, precisamos instalar o .Net Core Runtime. A versão mais recente atual do .Net Core é 3.1 (mas acho que não precisamos mais disso com .NET 6.0 né?) e você pode instalá-lo com o seguinte comando: 


`  `sudo apt-get update; \

`  `sudo apt-get install -y apt-transport-https && \

`  `sudo apt-get update && \

`  `sudo apt-get install -y aspnetcore-runtime-3.1

Instruções detalhadas e guia de solução de problemas está [aqui ](https://docs.microsoft.com/en-gb/dotnet/core/install/linux-ubuntu). 
## **Instalar o Nginx** 
NginX, é um servidor web que também pode ser usado como um proxy reverso, balanceador de carga, proxy de e-mail e cache HTTP. 

Você pode instalá-lo com um comando a seguir: 

<pre><code>sudo apt-get install nginx</code></pre>


ou a [instrução ](https://www.nginx.com/resources/wiki/start/topics/tutorials/install/#official-debian-ubuntu-packages)completa está localizada em um site oficial. 

Inicie o Nginx com um comando: 

<pre><code>sudo service nginx start</code></pre>

## **Configurar Nginx** 
Para configurar o Nginx como um proxy reverso para encaminhar solicitações ao seu aplicativo ASP.NET Core, modifique */etc/nginx/sites disponíveis/padrão*. Abra-o em um editor de texto e substitua o conteúdo pelo seguinte: 

Se você quiser obter mais detalhes sobre o que é proxy reverso e por que precisamos usá-lo leia este [artigo](https://en.wikipedia.org/wiki/Reverse_proxy). 

<pre><code>sudo nano /etc/nginx/sites-available/default</code></pre>


Adicione o seguinte codigo: 
<hr>

<pre><code>server {
    listen        80;
    server_name   seu_dominio;
    location / {
        proxy_pass         http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
}</code></pre>

<hr>
Salve o arquivo e verifique a sintaxe com o seguinte comando: 

<pre><code>sudo nginx -t</code></pre>

Se tudo estiver ok, reprise o Ngnix para aplicar novas configurações: 

<pre><code>sudo nginx -s reload</code></pre>

## **Configure api web .Net Core** 
Em primeiro lugar, você precisará construir uma api web no modo de liberação em sua máquina. Para fazer isso execute o comando na localização raiz do seu aplicativo: 

<pre><code>sudo dotnet publish --configuration Release</code></pre>

Crie uma pasta no Ubuntu **/var/www/\_app\_nome**

Copie todos os arquivos para essa pasta. Você pode usar FTP (se estiver disponível em seu VM) ou SSH File Transfer Protocol (estou usando GitHub para isso). 

Em terminal navegue até sua pasta de aplicação e execute o aplicativo. Isto é apenas para verificar se funciona: 

<pre><code>sudo dotnet "app_assembly.dll"</code></pre>

Se o aplicativo começou sem problemas pressione CTRL + C para pará-lo. 

Queremos que nosso aplicativo seja executado a partir de serviços. Desta forma, podemos instruir o Ubuntu a reiniciar nosso aplicativo automaticamente se ele falhar ou após a reinicialização do VM. 

Crie um arquivo de serviço: 

<pre><code>sudo nano /etc/systemd/system/kestrel-seu_app.service</code></pre>

Copie e cole este codigo com informações de seu app:

<hr>

<pre class="wp-block-code"><code>[Unit]
Description=&lt;App Nome&gt;

[Service]
WorkingDirectory=/var/www/&lt;app_diretorio&gt;
ExecStart=/usr/bin/dotnet /var/www/&lt;app_diretorio&gt;/&lt;app_nome&gt;.dll
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnet-example
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target</code></pre>

<hr>
Salve o arquivo do que ativar e iniciar o serviço: 

<pre><code>sudo systemctl enable kestrel-seu_app.service</code></pre>

<pre><code>sudo systemctl start kestrel-seu_app.service</code></pre>

Você pode verificar o status do serviço com o seguinte comando: 

<pre><code>sudo systemctl status kestrel-seu_app.service</code></pre>

**Isso é muito importante para liberar a porta 80 e o projeto funcionar!**

<pre><code>sudo iptables -I INPUT 6 -m state --state NEW -p tcp --dport 80 -j ACCEPT</code></pre>
