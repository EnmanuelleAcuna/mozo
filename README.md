# Mozo
Company management
Plataforma web para la gestion, control administrativo, financiero, de recursos humanos, contable, planillas, proyectos, para una PYME, mediana empresa, cooperativa, asociacion solidarista o empresa en general.

# Organizacion en /src
# Front end
- Web MVC

# Backend
- Web API
- Models
- Core
- Data

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and test
# Unit tests
Las pruebas unitarias existen para el proyecto Core, para probar la funcioalidad que se desarrolle ahi.

# Contribute
TODO: Explain how other users and developers can contribute to make your code better.

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)

dotnet ef dbcontext scaffold "Server=srv-mysql-ea.mysql.database.azure.com;User=azureuser;Password=Talamanca08;Database=Mozo;SslMode=Preferred;TreatTinyAsBoolean=true;" Pomelo.EntityFrameworkCore.MySql -o EntityFramework -f -c Mozo_DB_Context

azureuser Talamanca08