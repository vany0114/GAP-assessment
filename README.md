# GAP-assessment

## Resources and credentials
* Api: http://localhost:32880/swagger
* Website: http://localhost:32881
* Identity server: http://localhost:32883
* Sql Server:

| Ip  | User  | Password  |
|---|---|---|
| tcp:127.0.0.1,5433  | sa  | Pass@word  |

* IdentityDB: Gap.IdentityDb
* ApiDB: Gap.Insurance
* Default user/password: demouser@microsoft.com/Pass@word1

## Prerequisites and Installation Requirements
1. Install [Docker for Windows](https://docs.docker.com/docker-for-windows/install/).
2. Install [.NET Core 2.1](https://www.microsoft.com/net/download)
3. Install [Visual Studio 2017 15.7 or newer](https://docs.microsoft.com/en-us/visualstudio/install/update-visual-studio?view=vs-2017)
4. [Share drives in Docker settings, in order to deploy and debug with Visual Studio 2017 (See the below picture)](#tuning-docker-for-better-performance)
5. [Configure firewall](#configuring-the-firewall)
6. Press F5 and that's it!

![](https://github.com/vany0114/vany0114.github.io/blob/master/images/docker_settings_shared_drives.png)

> Note: The first time you hit F5 it'll take a few minutes, because in addition to compile the solution, it needs to pull/download the base images (SQL for Linux Docker, ASPNET, MongoDb and RabbitMQ images) and register them in the local image repo of your PC. The next time you hit F5 it'll be much faster.

### Tuning Docker for better performance

It is important to set Docker up properly with enough memory RAM and CPU assigned to it in order to improve the performance, or you will get errors when starting the containers with VS 2017 or "docker-compose up". Once Docker for Windows is installed in your machine, enter into its Settings and the Advanced menu option so you are able to adjust it to the minimum amount of memory and CPU (Memory: Around 4096MB and CPU:3) as shown in the image.

![](https://github.com/vany0114/vany0114.github.io/blob/master/images/docker_settings.png)

### Configuring the Firewall

IMPORTANT: Authentication to the STS (Security Token Service container) uses the 10.0.75.1 IP which should be available and already setup by Docker, so you need to set windows firewall to not protect DockerNAT or merely disable the firewall (this is only required to execute locally with Docker):

![](https://user-images.githubusercontent.com/5191049/36141845-8fa45666-10a6-11e8-9be0-0ec82747b445.png)

### Demo Screenshots
#### Api
![](https://github.com/vany0114/GAP-assessment/blob/master/Screenshots/api.png)
#### Api concent
![](https://github.com/vany0114/GAP-assessment/blob/master/Screenshots/api-consent.png)
#### Identity server
![](https://github.com/vany0114/GAP-assessment/blob/master/Screenshots/identityServer.png)
#### Identity login
![](https://github.com/vany0114/GAP-assessment/blob/master/Screenshots/identity-login.png)
#### Web-customers
![](https://github.com/vany0114/GAP-assessment/blob/master/Screenshots/web-customers.png)
#### Web-insurances
![](https://github.com/vany0114/GAP-assessment/blob/master/Screenshots/web-insurances.png)
